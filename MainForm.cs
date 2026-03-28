using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CrimsonDesertExpander
{
    public class MainForm : Form
    {
        // ─── Colors ───────────────────────────────────────────────────────────
        static readonly Color C_BG        = Color.FromArgb(12, 14, 20);
        static readonly Color C_PANEL     = Color.FromArgb(20, 24, 34);
        static readonly Color C_BORDER    = Color.FromArgb(42, 52, 72);
        static readonly Color C_ACCENT    = Color.FromArgb(55, 110, 200);
        static readonly Color C_ACCENT2   = Color.FromArgb(90, 160, 240);
        static readonly Color C_GOLD      = Color.FromArgb(200, 160, 60);
        static readonly Color C_TEXT      = Color.FromArgb(225, 228, 235);
        static readonly Color C_MUTED     = Color.FromArgb(100, 110, 130);
        static readonly Color C_SUCCESS   = Color.FromArgb(70, 195, 120);
        static readonly Color C_WARNING   = Color.FromArgb(220, 165, 45);
        static readonly Color C_ERROR     = Color.FromArgb(210, 70, 70);
        static readonly Color C_INPUT_BG  = Color.FromArgb(26, 30, 44);
        static readonly Color C_INPUT_FG  = Color.FromArgb(210, 215, 225);
        static readonly Color C_STORAGE   = Color.FromArgb(160, 100, 210); // violet pour le coffre

        // ─── Fonts ────────────────────────────────────────────────────────────
        Font _fontTitle    = new Font("Segoe UI", 15f, FontStyle.Bold);
        Font _fontSubtitle = new Font("Segoe UI", 8.5f, FontStyle.Regular);
        Font _fontLabel    = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold);
        Font _fontNormal   = new Font("Segoe UI", 8.5f);
        Font _fontSmall    = new Font("Segoe UI", 7.5f);
        Font _fontMono     = new Font("Consolas", 9f, FontStyle.Bold);
        Font _fontBtn      = new Font("Segoe UI Semibold", 9f, FontStyle.Bold);

        // ─── Controls — Inventory (left) ──────────────────────────────────────
        TextBox       _txtPath;
        Button        _btnBrowse, _btnDetect;
        Label         _lblCurDefault, _lblCurMax, _lblCurWarehouse, _lblBackupStatus;
        RadioButton   _rb1, _rb2, _rb3, _rb4, _rb5;
        // stubs inutilisés conservés pour compatibilité
        RadioButton   _rbRecommended, _rbVanilla, _rbMax, _rbUnlocked,
                      _rbL1, _rbL2, _rbL3, _rbR1, _rbR2, _rbR3;
        Panel         _pnlInvGroup;
        CheckBox      _chkEnableInv;

        // ─── Controls — Private Storage (right) ───────────────────────────────
        RadioButton   _rbWh1, _rbWh2, _rbWh3, _rbWh4, _rbWh5, _rbWh6, _rbWh7, _rbWh8;
        Panel         _pnlWhGroup;
        CheckBox      _chkEnableWh;

        // ─── Shared controls ──────────────────────────────────────────────────
        CheckBox  _chkBackup;
        Button    _btnApply, _btnRestore;
        Label     _lblStatus;
        Panel     _pnlLog;
        RichTextBox _rtbLog;
        Button    _btnFR, _btnEN, _btnES, _btnZH;

        // Panels fixes
        Panel _pnlPath, _pnlCurrent, _pnlPreset, _pnlStatus;

        // Quick repatch banner
        Panel  _pnlQuickRepatch;
        Label  _lblQuickInfo, _lblQuickTitle;
        Button _btnQuickRepatch;

        // Labels mis à jour lors du changement de langue
        Label _lblPathSection, _lblCurrentSection, _lblPresetSection,
              _lblBackupSection,
              _lblColLeft, _lblColRight,
              _lblCurDefaultKey, _lblCurMaxKey, _lblCurWarehouseKey,
              _lblHint;

        // ─────────────────────────────────────────────────────────────────────
        //  FORM WIDTH
        // ─────────────────────────────────────────────────────────────────────
        const int FORM_W    = 740;  // élargi pour 2 colonnes
        const int COL_MID   = 380;  // séparateur vertical dans _pnlPreset
        const int PANEL_W   = 700;  // largeur des panels (FORM_W - 2*20)

        public MainForm()
        {
            InitializeComponent();
            ApplyLanguage();
            AutoDetectOnLoad();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  INIT
        // ─────────────────────────────────────────────────────────────────────
        void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text            = "Crimson Desert — Inventory Expander v2.0.0";
            this.BackColor       = C_BG;
            this.ForeColor       = C_TEXT;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Font            = _fontNormal;

            try { this.Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location); }
            catch { }

            int y = 0;

            // ── Header ──────────────────────────────────────────────────────
            var pnlHeader = MakePanel(0, y, FORM_W, 80, C_PANEL);
            pnlHeader.Paint += (s, e) => PaintHeaderAccent(e.Graphics, pnlHeader);
            var lblTitle = MakeLabel("Crimson Desert", 20, 14, _fontTitle, C_TEXT);
            var lblSub   = MakeLabel("Inventory Expander v2.0.0", 20, 42, _fontSubtitle, C_MUTED);

            _btnEN = MakeLangButton("EN",   FORM_W - 174, 15);
            _btnFR = MakeLangButton("FR",   FORM_W - 140, 15);
            _btnES = MakeLangButton("ES",   FORM_W - 106, 15);
            _btnZH = MakeLangButton("中文", FORM_W -  72, 15);
            _btnEN.Size = _btnFR.Size = _btnES.Size = new Size(32, 22);
            _btnZH.Size = new Size(50, 22);

            _btnEN.Click += (s, e) => { Loc.Current = Language.EN; ApplyLanguage(); };
            _btnFR.Click += (s, e) => { Loc.Current = Language.FR; ApplyLanguage(); };
            _btnES.Click += (s, e) => { Loc.Current = Language.ES; ApplyLanguage(); };
            _btnZH.Click += (s, e) => { Loc.Current = Language.ZH; ApplyLanguage(); };

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblSub, _btnEN, _btnFR, _btnES, _btnZH });
            this.Controls.Add(pnlHeader);
            y += 80;

            // ── Quick repatch banner ─────────────────────────────────────────
            _pnlQuickRepatch = MakePanel(20, y + 10, PANEL_W, 46, C_INPUT_BG);
            _pnlQuickRepatch.Paint += (s, e) => PaintQuickRepatchBanner(e.Graphics, _pnlQuickRepatch);

            _lblQuickTitle = MakeLabel("", 20, 6,  new Font("Segoe UI Semibold", 7.5f, FontStyle.Bold), C_MUTED);
            _lblQuickInfo  = MakeLabel("", 20, 22, _fontNormal, C_MUTED);

            _btnQuickRepatch = new Button {
                Size      = new Size(120, 30), Location = new Point(PANEL_W - 135, 8),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(40, 110, 60),
                ForeColor = Color.White, Font = _fontBtn, Cursor = Cursors.Hand,
                Text = "", Visible = false
            };
            _btnQuickRepatch.FlatAppearance.BorderColor = Color.FromArgb(60, 160, 90);
            _btnQuickRepatch.FlatAppearance.BorderSize  = 1;
            _btnQuickRepatch.Click += BtnQuickRepatch_Click;

            _pnlQuickRepatch.Controls.AddRange(new Control[] { _lblQuickTitle, _lblQuickInfo, _btnQuickRepatch });
            this.Controls.Add(_pnlQuickRepatch);
            y += 66;

            // ── Chemin ──────────────────────────────────────────────────────
            y += 12;
            _lblPathSection = MakeSectionLabel("", 20, y);
            this.Controls.Add(_lblPathSection);
            y += 20;

            _pnlPath = MakePanel(20, y, PANEL_W, 40, C_INPUT_BG);
            _pnlPath.Name = "pnlPath";
            _pnlPath.Paint += (s, e) => PaintBorder(e.Graphics, _pnlPath, C_BORDER);
            _txtPath = new TextBox {
                Location  = new Point(10, 10), Size = new Size(PANEL_W - 230, 22),
                BackColor = C_INPUT_BG, ForeColor = C_INPUT_FG,
                BorderStyle = BorderStyle.None, Font = _fontNormal
            };
            _btnBrowse = MakeSmallButton("", PANEL_W - 208, 8, 92);
            _btnDetect = MakeSmallButton("", PANEL_W - 112, 8, 92);
            _btnBrowse.Click += BtnBrowse_Click;
            _btnDetect.Click += BtnDetect_Click;
            _pnlPath.Controls.AddRange(new Control[] { _txtPath, _btnBrowse, _btnDetect });
            this.Controls.Add(_pnlPath);
            y += 48;

            // ── Valeurs actuelles ────────────────────────────────────────────
            y += 10;
            _lblCurrentSection = MakeSectionLabel("", 20, y);
            this.Controls.Add(_lblCurrentSection);
            y += 20;

            _pnlCurrent = MakePanel(20, y, PANEL_W, 68, C_PANEL);
            _pnlCurrent.Name = "pnlCurrent";
            _pnlCurrent.Paint += (s, e) => PaintBorder(e.Graphics, _pnlCurrent, C_BORDER);

            // Colonne gauche (inventaire)
            _lblCurDefaultKey   = MakeLabel("", 14,  10, _fontLabel, C_MUTED);
            _lblCurDefault      = MakeLabel("—", 240, 10, _fontMono,  C_TEXT);
            _lblCurMaxKey       = MakeLabel("", 14,  30, _fontLabel, C_MUTED);
            _lblCurMax          = MakeLabel("—", 240, 30, _fontMono,  C_TEXT);

            // Colonne droite (coffre)
            _lblCurWarehouseKey = MakeLabel("", COL_MID + 14, 10, _fontLabel, C_STORAGE);
            _lblCurWarehouse    = MakeLabel("—", COL_MID + 160, 10, _fontMono, C_TEXT);

            _lblBackupStatus    = MakeLabel("", COL_MID + 14, 48, _fontSmall, C_MUTED);

            _pnlCurrent.Controls.AddRange(new Control[] {
                _lblCurDefaultKey, _lblCurDefault,
                _lblCurMaxKey,     _lblCurMax,
                _lblCurWarehouseKey, _lblCurWarehouse,
                _lblBackupStatus
            });
            this.Controls.Add(_pnlCurrent);
            y += 76;

            // ── Préréglages — deux colonnes ──────────────────────────────────
            y += 10;
            _lblPresetSection = MakeSectionLabel("", 20, y);
            this.Controls.Add(_lblPresetSection);
            y += 20;

            // Inventaire : 5 presets, coffre : 8 presets — coffre est le plus haut
            // 8 radios × 24 + header 28 + padding 8 = 228
            const int PRESET_H    = 228;
            const int RADIO_STEP  = 24;

            _pnlPreset = MakePanel(20, y, PANEL_W, PRESET_H, C_PANEL);
            _pnlPreset.Name = "pnlPreset";
            _pnlPreset.Paint += (s, e) => PaintPresetColumns(e.Graphics, _pnlPreset);

            // ── Colonne gauche : inventaire ──────────────────────────────────
            _lblColLeft    = MakeLabel("", 14, 6, new Font("Segoe UI Semibold", 8f, FontStyle.Bold), C_GOLD);
            _chkEnableInv  = MakeEnableCheckBox("", COL_MID - 110, 4, true);
            _chkEnableInv.CheckedChanged += ChkEnableInv_Changed;

            _pnlInvGroup = new Panel { Location = new Point(0, 28), Size = new Size(COL_MID, PRESET_H - 28), BackColor = Color.Transparent };

            _rb1 = MakeRadio("", 14, 4 + RADIO_STEP * 0, true);
            _rb2 = MakeRadio("", 14, 4 + RADIO_STEP * 1, false);
            _rb3 = MakeRadio("", 14, 4 + RADIO_STEP * 2, false);
            _rb4 = MakeRadio("", 14, 4 + RADIO_STEP * 3, false);
            _rb5 = MakeRadio("", 14, 4 + RADIO_STEP * 4, false);

            // stubs
            _rbRecommended = new RadioButton { Visible = false };
            _rbVanilla     = new RadioButton { Visible = false };
            _rbMax         = new RadioButton { Visible = false };
            _rbUnlocked    = new RadioButton { Visible = false };
            _rbL1 = new RadioButton { Visible = false };
            _rbL2 = new RadioButton { Visible = false };
            _rbL3 = new RadioButton { Visible = false };
            _rbR1 = new RadioButton { Visible = false };
            _rbR2 = new RadioButton { Visible = false };
            _rbR3 = new RadioButton { Visible = false };

            foreach (var rb in new[] { _rb1, _rb2, _rb3, _rb4, _rb5 })
                rb.CheckedChanged += Preset_Changed;

            _pnlInvGroup.Controls.AddRange(new Control[] { _rb1, _rb2, _rb3, _rb4, _rb5 });

            // ── Colonne droite : coffre privé ────────────────────────────────
            _lblColRight  = MakeLabel("", COL_MID + 14, 6, new Font("Segoe UI Semibold", 8f, FontStyle.Bold), C_STORAGE);
            _chkEnableWh  = MakeEnableCheckBox("", PANEL_W - 110, 4, true);
            _chkEnableWh.CheckedChanged += ChkEnableWh_Changed;

            _pnlWhGroup = new Panel { Location = new Point(COL_MID, 28), Size = new Size(PANEL_W - COL_MID, PRESET_H - 28), BackColor = Color.Transparent };

            _rbWh1 = MakeRadio("", 14, 4 + RADIO_STEP * 0, true);
            _rbWh2 = MakeRadio("", 14, 4 + RADIO_STEP * 1, false);
            _rbWh3 = MakeRadio("", 14, 4 + RADIO_STEP * 2, false);
            _rbWh4 = MakeRadio("", 14, 4 + RADIO_STEP * 3, false);
            _rbWh5 = MakeRadio("", 14, 4 + RADIO_STEP * 4, false);
            _rbWh6 = MakeRadio("", 14, 4 + RADIO_STEP * 5, false);
            _rbWh7 = MakeRadio("", 14, 4 + RADIO_STEP * 6, false);
            _rbWh8 = MakeRadio("", 14, 4 + RADIO_STEP * 7, false);

            foreach (var rb in new[] { _rbWh1, _rbWh2, _rbWh3, _rbWh4, _rbWh5, _rbWh6, _rbWh7, _rbWh8 })
                rb.CheckedChanged += WhPreset_Changed;

            _pnlWhGroup.Controls.AddRange(new Control[] { _rbWh1, _rbWh2, _rbWh3, _rbWh4, _rbWh5, _rbWh6, _rbWh7, _rbWh8 });

            _pnlPreset.Controls.AddRange(new Control[] {
                _lblColLeft, _chkEnableInv, _pnlInvGroup,
                _lblColRight, _chkEnableWh, _pnlWhGroup
            });
            this.Controls.Add(_pnlPreset);
            y += PRESET_H + 8;

            // ── Sauvegarde ───────────────────────────────────────────────────
            _lblBackupSection = MakeSectionLabel("", 20, y);
            this.Controls.Add(_lblBackupSection);

            _chkBackup = new CheckBox {
                Size      = new Size(560, 22),
                ForeColor = C_TEXT, BackColor = Color.Transparent,
                Checked   = true, Font = _fontNormal
            };
            this.Controls.Add(_chkBackup);

            // ── Boutons d'action ─────────────────────────────────────────────
            _btnApply = new Button {
                Size      = new Size(338, 42), Text = "",
                FlatStyle = FlatStyle.Flat, BackColor = C_ACCENT,
                ForeColor = Color.White, Font = _fontBtn, Cursor = Cursors.Hand
            };
            _btnApply.FlatAppearance.BorderSize = 0;
            _btnApply.Paint  += PaintGlowButton;
            _btnApply.Click  += BtnApply_Click;

            _btnRestore = new Button {
                Size      = new Size(348, 42), Text = "",
                FlatStyle = FlatStyle.Flat, BackColor = C_INPUT_BG,
                ForeColor = C_MUTED, Font = _fontBtn, Cursor = Cursors.Hand
            };
            _btnRestore.FlatAppearance.BorderColor = C_BORDER;
            _btnRestore.FlatAppearance.BorderSize  = 1;
            _btnRestore.Click += BtnRestore_Click;

            this.Controls.AddRange(new Control[] { _btnApply, _btnRestore });

            // ── Status ───────────────────────────────────────────────────────
            _pnlStatus = MakePanel(20, 0, PANEL_W, 30, C_INPUT_BG);
            _pnlStatus.Paint += (s, e) => PaintBorder(e.Graphics, _pnlStatus, C_BORDER);
            _pnlStatus.Name = "pnlStatus";
            _lblStatus = MakeLabel("", 12, 8, _fontNormal, C_MUTED);
            _pnlStatus.Controls.Add(_lblStatus);
            this.Controls.Add(_pnlStatus);

            // ── Log ──────────────────────────────────────────────────────────
            _pnlLog = MakePanel(20, 0, PANEL_W, 80, C_INPUT_BG);
            _pnlLog.Paint += (s, e) => PaintBorder(e.Graphics, _pnlLog, C_BORDER);
            _rtbLog = new RichTextBox {
                Location = new Point(6, 6), Size = new Size(PANEL_W - 12, 68),
                BackColor = C_INPUT_BG, ForeColor = C_MUTED,
                BorderStyle = BorderStyle.None, ReadOnly = true,
                Font = new Font("Consolas", 7.5f), ScrollBars = RichTextBoxScrollBars.Vertical
            };
            _pnlLog.Controls.Add(_rtbLog);
            this.Controls.Add(_pnlLog);

            // ── Hint ─────────────────────────────────────────────────────────
            _lblHint = MakeLabel("", 20, 0, _fontSmall, C_MUTED);
            this.Controls.Add(_lblHint);

            this.ResumeLayout(false);
            RelayoutFromPreset();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  RELAYOUT
        // ─────────────────────────────────────────────────────────────────────
        void RelayoutFromPreset()
        {
            // Header(80) + bannière(66) = 146 toujours
            int y = 146;

            y += 12;
            _lblPathSection.Location = new Point(20, y);
            y += 20;
            _pnlPath.Location = new Point(20, y);
            y += 48;

            y += 10;
            _lblCurrentSection.Location = new Point(20, y);
            y += 20;
            _pnlCurrent.Location = new Point(20, y);
            y += 76;

            y += 10;
            _lblPresetSection.Location = new Point(20, y);
            y += 20;
            _pnlPreset.Location = new Point(20, y);
            y += 248;

            y += 10;
            _lblBackupSection.Location = new Point(20, y);
            y += 20;
            _chkBackup.Location = new Point(22, y);
            y += 30;

            y += 6;
            _btnApply.Location   = new Point(20, y);
            _btnRestore.Location = new Point(372, y);
            y += 50;

            y += 6;
            _pnlStatus.Location = new Point(20, y);
            y += 38;

            _pnlLog.Location = new Point(20, y);
            y += 88;

            _lblHint.Location = new Point(20, y);
            y += 22;

            this.MinimumSize = new Size(FORM_W, y + 10);
            this.MaximumSize = new Size(FORM_W, y + 10);
            this.Size        = new Size(FORM_W, y + 10);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  LANGUAGE
        // ─────────────────────────────────────────────────────────────────────
        void ApplyLanguage()
        {
            _lblPathSection.Text    = Loc.Get("game_path_label").ToUpper();
            _lblCurrentSection.Text = Loc.Get("current_values").ToUpper();
            _lblPresetSection.Text  = Loc.Get("preset_label").ToUpper();
            _lblBackupSection.Text  = Loc.Get("backup_label").ToUpper();

            _btnBrowse.Text = Loc.Get("browse");
            _btnDetect.Text = Loc.Get("detect");

            // Colonne gauche
            _lblColLeft.Text   = Loc.Get("preset_max_label");
            _chkEnableInv.Text = Loc.Get("enable_patch");
            _rb1.Text = Loc.Get("preset_p1");
            _rb2.Text = Loc.Get("preset_p2");
            _rb3.Text = Loc.Get("preset_p3");
            _rb4.Text = Loc.Get("preset_p4");
            _rb5.Text = Loc.Get("preset_p5");

            // Colonne droite
            _lblColRight.Text  = Loc.Get("preset_wh_label");
            _chkEnableWh.Text  = Loc.Get("enable_patch");
            _rbWh1.Text = Loc.Get("preset_wh1");
            _rbWh2.Text = Loc.Get("preset_wh2");
            _rbWh3.Text = Loc.Get("preset_wh3");
            _rbWh4.Text = Loc.Get("preset_wh4");
            _rbWh5.Text = Loc.Get("preset_wh5");
            _rbWh6.Text = Loc.Get("preset_wh6");
            _rbWh7.Text = Loc.Get("preset_wh7");
            _rbWh8.Text = Loc.Get("preset_wh8");

            // stubs
            _rbRecommended.Text = ""; _rbVanilla.Text = ""; _rbMax.Text = ""; _rbUnlocked.Text = "";
            _rbL1.Text = ""; _rbL2.Text = ""; _rbL3.Text = "";
            _rbR1.Text = ""; _rbR2.Text = ""; _rbR3.Text = "";

            // Valeurs actuelles
            _lblCurDefaultKey.Text   = Loc.Get("starting_slots") + " :";
            _lblCurMaxKey.Text       = Loc.Get("max_slots") + " :";
            _lblCurWarehouseKey.Text = Loc.Get("warehouse_slots") + " :";

            _chkBackup.Text   = Loc.Get("backup_auto");
            _btnApply.Text    = Loc.Get("apply_patch");
            _btnRestore.Text  = Loc.Get("restore_backup");
            _lblHint.Text     = Loc.Get("uninstall_hint");

            SetStatus(Loc.Get("status_ready"), C_MUTED);
            UpdateBackupStatus();

            _btnEN.ForeColor = Loc.Current == Language.EN ? C_ACCENT2 : C_MUTED;
            _btnFR.ForeColor = Loc.Current == Language.FR ? C_ACCENT2 : C_MUTED;
            _btnES.ForeColor = Loc.Current == Language.ES ? C_ACCENT2 : C_MUTED;
            _btnZH.ForeColor = Loc.Current == Language.ZH ? C_ACCENT2 : C_MUTED;

            RefreshQuickRepatchBanner();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  AUTO-DETECT
        // ─────────────────────────────────────────────────────────────────────
        void AutoDetectOnLoad()
        {
            SetStatus(Loc.Get("status_detecting"), C_MUTED);

            var cfg = ConfigManager.Load();
            if (cfg != null && !string.IsNullOrEmpty(cfg.GamePath))
            {
                _txtPath.Text = cfg.GamePath;
                Log($"[CFG] {Loc.Get("log_config_loaded")} {cfg.PatchedAt:yyyy-MM-dd HH:mm}");
            }
            else
            {
                var path = PatchEngine.DetectGamePath();
                if (path != null) _txtPath.Text = path;
            }

            if (!string.IsNullOrEmpty(_txtPath.Text) && PatchEngine.PazExists(_txtPath.Text))
            {
                SetStatus(Loc.Get("status_found"), C_SUCCESS);
                Log($"[AUTO] {_txtPath.Text}");
                LoadCurrentValues();
            }
            else
            {
                SetStatus(Loc.Get("status_not_found"), C_WARNING);
            }

            RefreshQuickRepatchBanner();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  EVENTS
        // ─────────────────────────────────────────────────────────────────────
        void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog {
                Description = Loc.Get("select_folder"),
                ShowNewFolderButton = false
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _txtPath.Text = dlg.SelectedPath;
                    LoadCurrentValues();
                }
            }
        }

        void BtnDetect_Click(object sender, EventArgs e)
        {
            SetStatus(Loc.Get("status_detecting"), C_MUTED);
            var path = PatchEngine.DetectGamePath();
            if (path != null)
            {
                _txtPath.Text = path;
                SetStatus(Loc.Get("status_found"), C_SUCCESS);
                Log($"[DETECT] {path}");
                LoadCurrentValues();
            }
            else
            {
                SetStatus(Loc.Get("status_not_found"), C_WARNING);
                Log("[DETECT] Game not found.");
            }
        }

        void Preset_Changed(object sender, EventArgs e)
        {
            // Rien à faire — plus de panel custom
        }

        void WhPreset_Changed(object sender, EventArgs e)
        {
            // Rien à faire — plus de panel custom
        }

        void ChkEnableInv_Changed(object sender, EventArgs e)
        {
            bool on = _chkEnableInv.Checked;
            foreach (Control c in _pnlInvGroup.Controls) c.Enabled = on;
        }

        void ChkEnableWh_Changed(object sender, EventArgs e)
        {
            bool on = _chkEnableWh.Checked;
            foreach (Control c in _pnlWhGroup.Controls) c.Enabled = on;
        }
        void BtnApply_Click(object sender, EventArgs e)
        {
            var path = _txtPath.Text.Trim();
            if (string.IsNullOrEmpty(path))      { SetStatus(Loc.Get("err_no_path"),        C_ERROR); return; }
            if (!PatchEngine.PazExists(path))     { SetStatus(Loc.Get("err_file_not_found"), C_ERROR); return; }
            if (!_chkEnableInv.Checked && !_chkEnableWh.Checked)
            {
                SetStatus(Loc.Get("err_nothing_selected"), C_WARNING);
                return;
            }

            // Lire les valeurs actuelles pour les sections non modifiées
            PazValues current = null;
            try { current = PatchEngine.ReadValues(path); }
            catch (Exception ex) { SetStatus(Loc.Get("err_patch_failed") + ex.Message, C_ERROR); return; }

            ushort defSlots, maxSlots, whSlots;

            // Inventaire
            if (_chkEnableInv.Checked)
            {
                if      (_rb1.Checked) { defSlots =  50; maxSlots = 240; }
                else if (_rb2.Checked) { defSlots = 100; maxSlots = 240; }
                else if (_rb3.Checked) { defSlots = 150; maxSlots = 240; }
                else if (_rb4.Checked) { defSlots = 200; maxSlots = 240; }
                else                   { defSlots = 250; maxSlots = 300; } // _rb5
            }
            else { defSlots = current.DefaultSlots; maxSlots = current.MaxSlots; }

            // Coffre privé
            if (_chkEnableWh.Checked)
            {
                if      (_rbWh1.Checked) whSlots = 240;
                else if (_rbWh2.Checked) whSlots = 300;
                else if (_rbWh3.Checked) whSlots = 400;
                else if (_rbWh4.Checked) whSlots = 500;
                else if (_rbWh5.Checked) whSlots = 700;
                else if (_rbWh6.Checked) whSlots = 800;
                else if (_rbWh7.Checked) whSlots = 900;
                else                     whSlots = 999; // _rbWh8
            }
            else { whSlots = current.WarehouseSlots; }

            var msg = string.Format(Loc.Get("confirm_patch_msg"), defSlots, maxSlots, whSlots);
            if (MessageBox.Show(msg, Loc.Get("confirm_patch_title"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                Log(Loc.Get("log_writing"));

                if (_chkBackup.Checked)
                {
                    bool existed = PatchEngine.BackupExists(path);
                    PatchEngine.CreateBackup(path);
                    Log(existed
                        ? $"[BAK] {Loc.Get("status_backup_exists")}"
                        : $"[BAK] {Loc.Get("status_backup_created")}");
                }

                var result = PatchEngine.WriteValues(path, defSlots, maxSlots, whSlots);
                Log($"[OK] Inv start={defSlots} max={maxSlots}  |  Storage={whSlots}");
                Log(Loc.Get("log_verifying"));
                Log($"[VERIFY] Default={result.DefaultSlots} Max={result.MaxSlots} Warehouse={result.WarehouseSlots}");

                ConfigManager.Save(new PatchConfig {
                    GamePath       = path,
                    DefaultSlots   = defSlots,
                    MaxSlots       = maxSlots,
                    WarehouseSlots = whSlots,
                    PresetKey      = GetCurrentPresetKey(),
                    WPresetKey     = GetCurrentWhPresetKey(),
                    PatchedAt      = DateTime.Now
                });
                RefreshQuickRepatchBanner();

                SetStatus(Loc.Get("status_patched"), C_SUCCESS);
                LoadCurrentValues();
            }
            catch (Exception ex)
            {
                SetStatus(Loc.Get("err_patch_failed") + ex.Message, C_ERROR);
                Log($"[ERR] {ex.Message}");
            }
        }

        void BtnRestore_Click(object sender, EventArgs e)
        {
            var path = _txtPath.Text.Trim();
            if (string.IsNullOrEmpty(path))       { SetStatus(Loc.Get("err_no_path"),  C_ERROR); return; }
            if (!PatchEngine.BackupExists(path))   { SetStatus(Loc.Get("err_no_backup"), C_ERROR); return; }

            if (MessageBox.Show(Loc.Get("confirm_restore_msg"), Loc.Get("confirm_restore_title"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                PatchEngine.RestoreBackup(path);
                SetStatus(Loc.Get("status_restored"), C_SUCCESS);
                Log($"[RESTORE] {Loc.Get("status_restored")}");
                LoadCurrentValues();
            }
            catch (Exception ex)
            {
                SetStatus(Loc.Get("err_patch_failed") + ex.Message, C_ERROR);
                Log($"[ERR] {ex.Message}");
            }
        }

        void BtnQuickRepatch_Click(object sender, EventArgs e)
        {
            var cfg = ConfigManager.Load();
            if (cfg == null) return;

            var path = string.IsNullOrEmpty(_txtPath.Text) ? cfg.GamePath : _txtPath.Text.Trim();
            if (!PatchEngine.PazExists(path)) { SetStatus(Loc.Get("err_file_not_found"), C_ERROR); return; }

            try
            {
                if (_chkBackup.Checked) PatchEngine.CreateBackup(path);

                ushort wh = cfg.WarehouseSlots > 0 ? cfg.WarehouseSlots : (ushort)240;
                var result = PatchEngine.WriteValues(path, cfg.DefaultSlots, cfg.MaxSlots, wh);
                Log($"[QUICK] Inv={result.DefaultSlots}/{result.MaxSlots}  Storage={result.WarehouseSlots}");
                SetStatus(Loc.Get("status_patched"), C_SUCCESS);

                cfg.PatchedAt = DateTime.Now;
                cfg.GamePath  = path;
                ConfigManager.Save(cfg);
                RefreshQuickRepatchBanner();
                LoadCurrentValues();
            }
            catch (Exception ex)
            {
                SetStatus(Loc.Get("err_patch_failed") + ex.Message, C_ERROR);
                Log($"[ERR] {ex.Message}");
            }
        }

        void RefreshQuickRepatchBanner()
        {
            var cfg = ConfigManager.Load();

            if (cfg == null || cfg.DefaultSlots == 0)
            {
                _lblQuickTitle.Text      = Loc.Get("quick_no_patch_title");
                _lblQuickTitle.ForeColor = C_WARNING;
                _lblQuickInfo.Text       = Loc.Get("quick_no_patch_info");
                _lblQuickInfo.ForeColor  = C_MUTED;
                _btnQuickRepatch.Visible = false;
            }
            else
            {
                ushort wh = cfg.WarehouseSlots > 0 ? cfg.WarehouseSlots : (ushort)240;
                _lblQuickTitle.Text      = Loc.Get("quick_repatch_title");
                _lblQuickTitle.ForeColor = C_SUCCESS;
                _lblQuickInfo.Text       = $"Inv {cfg.DefaultSlots}/{cfg.MaxSlots}  |  Storage {wh}  —  {Loc.Get("quick_patched_on")} {cfg.PatchedAt:yyyy-MM-dd HH:mm}";
                _lblQuickInfo.ForeColor  = C_TEXT;
                _btnQuickRepatch.Text    = Loc.Get("quick_repatch_btn");
                _btnQuickRepatch.Visible = true;
            }

            _pnlQuickRepatch.Invalidate();
        }

        string GetCurrentPresetKey()
        {
            if (_rb1.Checked) return "p1";
            if (_rb2.Checked) return "p2";
            if (_rb3.Checked) return "p3";
            if (_rb4.Checked) return "p4";
            return "custom";
        }

        string GetCurrentWhPresetKey()
        {
            if (_rbWh1.Checked) return "wh1";
            if (_rbWh2.Checked) return "wh2";
            if (_rbWh3.Checked) return "wh3";
            if (_rbWh4.Checked) return "wh4";
            if (_rbWh5.Checked) return "wh5";
            if (_rbWh6.Checked) return "wh6";
            if (_rbWh7.Checked) return "wh7";
            return "wh8";
        }

        static string PresetKeyToLabel(string key, ushort def, ushort max)
        {
            switch (key)
            {
                case "p1": return Loc.Get("preset_p1");
                case "p2": return Loc.Get("preset_p2");
                case "p3": return Loc.Get("preset_p3");
                case "p4": return Loc.Get("preset_p4");
                case "p5": return Loc.Get("preset_p5");
                case "l1": case "r1": return Loc.Get("preset_p1");
                case "l2": case "r2": return Loc.Get("preset_p2");
                case "l3": case "r3": return Loc.Get("preset_p4");
                default:   return Loc.Get("preset_p1"); // fallback safe
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        //  HELPERS
        // ─────────────────────────────────────────────────────────────────────
        void LoadCurrentValues()
        {
            var path = _txtPath.Text.Trim();
            if (string.IsNullOrEmpty(path) || !PatchEngine.PazExists(path))
            {
                _lblCurDefault.Text   = "—";
                _lblCurMax.Text       = "—";
                _lblCurWarehouse.Text = "—";
                UpdateBackupStatus();
                return;
            }
            try
            {
                Log(Loc.Get("log_reading"));
                var vals = PatchEngine.ReadValues(path);
                _lblCurDefault.Text   = $"{vals.DefaultSlots}  ({Loc.Get("vanilla")}: 50)";
                _lblCurMax.Text       = $"{vals.MaxSlots}  ({Loc.Get("vanilla")}: 240)";
                _lblCurWarehouse.Text = $"{vals.WarehouseSlots}  ({Loc.Get("vanilla")}: 240)";
                UpdateBackupStatus();
                SelectMatchingRadio(vals.DefaultSlots, vals.MaxSlots, vals.WarehouseSlots);
            }
            catch (Exception ex)
            {
                _lblCurDefault.Text   = "ERR";
                _lblCurMax.Text       = "ERR";
                _lblCurWarehouse.Text = "ERR";
                Log($"[ERR] {ex.Message}");
            }
        }

        void SelectMatchingRadio(ushort def, ushort max, ushort wh)
        {
            foreach (var rb in new[] { _rb1, _rb2, _rb3, _rb4, _rb5 })
                rb.CheckedChanged -= Preset_Changed;
            foreach (var rb in new[] { _rbWh1, _rbWh2, _rbWh3, _rbWh4, _rbWh5, _rbWh6, _rbWh7, _rbWh8 })
                rb.CheckedChanged -= WhPreset_Changed;
            _chkEnableInv.CheckedChanged -= ChkEnableInv_Changed;
            _chkEnableWh.CheckedChanged  -= ChkEnableWh_Changed;

            // Inventaire — si valeur inconnue, sélectionner le preset le plus proche
            if      (def ==  50 && max == 240) _rb1.Checked = true;
            else if (def == 100 && max == 240) _rb2.Checked = true;
            else if (def == 150 && max == 240) _rb3.Checked = true;
            else if (def == 200 && max == 240) _rb4.Checked = true;
            else                               _rb5.Checked = true; // 250/300 ou valeur inconnue → Warlord

            // Coffre — si valeur inconnue, sélectionner le preset le plus proche
            if      (wh <= 240) _rbWh1.Checked = true;
            else if (wh <= 300) _rbWh2.Checked = true;
            else if (wh <= 400) _rbWh3.Checked = true;
            else if (wh <= 500) _rbWh4.Checked = true;
            else if (wh <= 700) _rbWh5.Checked = true;
            else if (wh <= 800) _rbWh6.Checked = true;
            else if (wh <= 900) _rbWh7.Checked = true;
            else                _rbWh8.Checked = true;

            foreach (var rb in new[] { _rb1, _rb2, _rb3, _rb4, _rb5 })
                rb.CheckedChanged += Preset_Changed;
            foreach (var rb in new[] { _rbWh1, _rbWh2, _rbWh3, _rbWh4, _rbWh5, _rbWh6, _rbWh7, _rbWh8 })
                rb.CheckedChanged += WhPreset_Changed;
            _chkEnableInv.CheckedChanged += ChkEnableInv_Changed;
            _chkEnableWh.CheckedChanged  += ChkEnableWh_Changed;

            foreach (Control c in _pnlInvGroup.Controls) c.Enabled = _chkEnableInv.Checked;
            foreach (Control c in _pnlWhGroup.Controls)  c.Enabled = _chkEnableWh.Checked;

            RelayoutFromPreset();
        }

        void UpdateBackupStatus()
        {
            var path = _txtPath.Text.Trim();
            if (!string.IsNullOrEmpty(path) && PatchEngine.BackupExists(path))
            {
                _lblBackupStatus.Text      = Loc.Get("backup_exists_info");
                _lblBackupStatus.ForeColor = C_SUCCESS;
                _btnRestore.ForeColor      = C_TEXT;
                _btnRestore.BackColor      = C_INPUT_BG;
            }
            else
            {
                _lblBackupStatus.Text      = Loc.Get("backup_missing_info");
                _lblBackupStatus.ForeColor = C_MUTED;
                _btnRestore.ForeColor      = C_MUTED;
                _btnRestore.BackColor      = C_INPUT_BG;
            }
        }

        void SetStatus(string msg, Color color)
        {
            _lblStatus.Text      = msg;
            _lblStatus.ForeColor = color;
        }

        void Log(string line)
        {
            var ts = DateTime.Now.ToString("HH:mm:ss");
            _rtbLog.AppendText($"[{ts}] {line}\n");
            _rtbLog.ScrollToCaret();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  FACTORY HELPERS
        // ─────────────────────────────────────────────────────────────────────
        Panel MakePanel(int x, int y, int w, int h, Color bg)
            => new Panel { Location = new Point(x, y), Size = new Size(w, h), BackColor = bg };

        Label MakeLabel(string text, int x, int y, Font font, Color color)
            => new Label {
                Text = text, Location = new Point(x, y), AutoSize = true,
                Font = font, ForeColor = color, BackColor = Color.Transparent
            };

        Label MakeSectionLabel(string text, int x, int y)
        {
            var lbl = MakeLabel(text, x, y, _fontSmall, C_GOLD);
            lbl.Font = new Font("Segoe UI Semibold", 7.5f, FontStyle.Bold);
            return lbl;
        }

        Button MakeSmallButton(string text, int x, int y, int w)
        {
            var btn = new Button {
                Text = text, Location = new Point(x, y), Size = new Size(w, 24),
                FlatStyle = FlatStyle.Flat, BackColor = C_PANEL,
                ForeColor = C_TEXT, Font = _fontSmall, Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = C_BORDER;
            btn.FlatAppearance.BorderSize  = 1;
            return btn;
        }

        Button MakeLangButton(string text, int x, int y)
        {
            var btn = new Button {
                Text = text, Location = new Point(x, y), Size = new Size(30, 22),
                FlatStyle = FlatStyle.Flat, BackColor = Color.Transparent,
                ForeColor = C_MUTED, Font = new Font("Segoe UI Semibold", 8f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            return btn;
        }

        CheckBox MakeEnableCheckBox(string text, int x, int y, bool chk)
            => new CheckBox {
                Text      = text,
                Location  = new Point(x, y),
                AutoSize  = true,
                Checked   = chk,
                ForeColor = C_MUTED,
                BackColor = Color.Transparent,
                Font      = _fontSmall,
                Cursor    = Cursors.Hand
            };

        RadioButton MakeRadio(string text, int x, int y, bool chk)
            => new RadioButton {
                Text = text, Location = new Point(x, y), AutoSize = true,
                Checked = chk, ForeColor = C_TEXT, BackColor = Color.Transparent,
                Font = _fontNormal, Cursor = Cursors.Hand
            };

        // ─────────────────────────────────────────────────────────────────────
        //  PAINT
        // ─────────────────────────────────────────────────────────────────────
        void PaintQuickRepatchBanner(Graphics g, Panel p)
        {
            bool hasPatch     = _btnQuickRepatch.Visible;
            Color bgColor     = hasPatch ? Color.FromArgb(18, 50, 28)  : Color.FromArgb(45, 32, 10);
            Color bgColor2    = hasPatch ? Color.FromArgb(22, 42, 26)  : Color.FromArgb(38, 28, 10);
            Color borderColor = hasPatch ? Color.FromArgb(50, 130, 70) : Color.FromArgb(140, 90, 20);
            Color barColor    = hasPatch ? Color.FromArgb(60, 160, 90) : Color.FromArgb(180, 120, 30);
            using (var brush = new LinearGradientBrush(new Rectangle(0, 0, p.Width, p.Height), bgColor, bgColor2, 90f))
                g.FillRectangle(brush, 0, 0, p.Width, p.Height);
            using (var pen = new Pen(borderColor, 1))
                g.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
            using (var barBrush = new SolidBrush(barColor))
                g.FillRectangle(barBrush, 0, 0, 4, p.Height);
        }

        void PaintPresetColumns(Graphics g, Panel p)
        {
            // Bordure extérieure
            using (var pen = new Pen(C_BORDER, 1))
                g.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
            // Séparateur vertical
            using (var pen = new Pen(C_BORDER, 1))
                g.DrawLine(pen, COL_MID, 4, COL_MID, p.Height - 4);
            // Barre gauche (or — inventaire)
            using (var b = new SolidBrush(C_GOLD))
                g.FillRectangle(b, 14, 4, 3, 20);
            // Barre droite (violet — coffre)
            using (var b = new SolidBrush(C_STORAGE))
                g.FillRectangle(b, COL_MID + 14, 4, 3, 20);
        }

        void PaintHeaderAccent(Graphics g, Panel p)
        {
            using (var brush = new LinearGradientBrush(
                new Point(0, p.Height - 3), new Point(p.Width, p.Height - 3), C_ACCENT, C_GOLD))
                g.FillRectangle(brush, 0, p.Height - 3, p.Width, 3);
        }

        void PaintBorder(Graphics g, Panel p, Color c)
        {
            using (var pen = new Pen(c, 1))
                g.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
        }

        void PaintGlowButton(object sender, PaintEventArgs e)
        {
            var btn  = (Button)sender;
            var rect = new Rectangle(0, 0, btn.Width, btn.Height);
            using (var brush = new LinearGradientBrush(rect, C_ACCENT, C_ACCENT2, 90f))
                e.Graphics.FillRectangle(brush, rect);
            using (var pen = new Pen(C_GOLD, 1f))
                e.Graphics.DrawLine(pen, 0, btn.Height - 1, btn.Width, btn.Height - 1);
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                e.Graphics.DrawString(btn.Text, _fontBtn, Brushes.White, (RectangleF)rect, sf);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _fontTitle.Dispose(); _fontSubtitle.Dispose();
            _fontLabel.Dispose(); _fontNormal.Dispose();
            _fontSmall.Dispose(); _fontMono.Dispose(); _fontBtn.Dispose();
            base.OnFormClosed(e);
        }
    }
}
