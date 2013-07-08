using System;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;

namespace Mars2WGS
{
    public partial class MainForm : Form
    {
        string AppPath = System.AppDomain.CurrentDomain.BaseDirectory;
        string LastOpenFolder = System.AppDomain.CurrentDomain.BaseDirectory;
        string LastSaveFolder = System.AppDomain.CurrentDomain.BaseDirectory;
        string LastMapSource = "Google Map";
        string LastConvertMode = "Mars2Wgs.txt";

        MarsWGS m2w = new MarsWGS();
        string suffix = "_wgs";

        private void LoadSetting()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration( Application.ExecutablePath );
            AppSettingsSection appSection = config.AppSettings;

            if ( appSection.Settings["LastOpenFolder"] != null )
            {
                LastOpenFolder = appSection.Settings["LastOpenFolder"].Value;
            }
            else
            {
                appSection.Settings.Add( "LastOpenFolder", LastOpenFolder );
            }

            if ( appSection.Settings["LastSaveFolder"] != null )
            {
                LastOpenFolder = appSection.Settings["LastSaveFolder"].Value;
            }
            else
            {
                appSection.Settings.Add( "LastSaveFolder", LastSaveFolder );
            }

            config.Save( ConfigurationSaveMode.Full );
        }

        private void SaveSetting()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration( Application.ExecutablePath );
            AppSettingsSection appSection = config.AppSettings;

            if ( appSection.Settings["LastOpenFolder"] != null )
            {
                appSection.Settings["LastOpenFolder"].Value = LastOpenFolder;
            }
            else
            {
                appSection.Settings.Add( "LastOpenFolder", LastOpenFolder );
            }

            if ( appSection.Settings["LastSaveFolder"] != null )
            {
                appSection.Settings["LastSaveFolder"].Value = LastSaveFolder;
            }
            else
            {
                appSection.Settings.Add( "LastSaveFolder", LastSaveFolder );
            }

            config.Save( ConfigurationSaveMode.Full );
        }

        public MainForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            LoadSetting();
            cbbConvertAlgorithm.SelectedIndex = 0;
            cbbMapSource.SelectedIndex = 0;
        }

        private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            SaveSetting();
        }

        private void btnToWGS_Click( object sender, EventArgs e )
        {
            double x = Convert.ToDouble( edSrcLon.Text );
            double y = Convert.ToDouble( edSrcLat.Text );

            double xWgs = x;
            double yWgs = y;

            m2w.Convert2WGS( x, y, out xWgs, out yWgs );

            edDstLon.Text = xWgs.ToString( "0.0000000" );
            edDstLat.Text = yWgs.ToString( "0.0000000" );

            edDiffLon.Text = ( xWgs - x ).ToString( "0.0000000" );
            edDiffLat.Text = ( yWgs - y ).ToString( "0.0000000" );
        }

        private void btnToMars_Click( object sender, EventArgs e )
        {
            double x = Convert.ToDouble( edSrcLon.Text );
            double y = Convert.ToDouble( edSrcLat.Text );

            double xMars = x;
            double yMars = y;

            m2w.Convert2Mars( x, y, out xMars, out yMars );

            edDstLon.Text = xMars.ToString( "0.0000000" );
            edDstLat.Text = yMars.ToString( "0.0000000" );

            edDiffLon.Text = ( xMars - x ).ToString( "0.0000000" );
            edDiffLat.Text = ( yMars - y ).ToString( "0.0000000" );
        }

        private void btnConvert_Click( object sender, EventArgs e )
        {
            //m2w.Convert2WGS( "test.gpx", "test_wgs.gpx" );

            string file_src = edFileSrc.Text.Trim();
            string file_dst = edFileDst.Text.Trim();

            m2w.Convert2WGS( file_src, file_dst );
        }

        private void edFileSrc_DragEnter( object sender, DragEventArgs e )
        {
            if ( e.Data.GetDataPresent( "FileDrop" ) )
                e.Effect = DragDropEffects.Copy;
        }

        private void edFileSrc_DragDrop( object sender, DragEventArgs e )
        {
            string[] filenames = (string[])e.Data.GetData( "FileDrop" );
            string file_src = filenames[0].Trim();
            string file_dst = filenames[0].Trim();

            string fps = System.IO.Path.GetDirectoryName( file_src );
            string fns = System.IO.Path.GetFileNameWithoutExtension(file_src);
            string fes = System.IO.Path.GetExtension(file_src);

            file_dst = string.Format( "{0}\\{1}{2}{3}", fps, fns, suffix, fes );

            edFileSrc.Text = file_src.Trim();
            edFileDst.Text = file_dst.Trim();
        }

        private void edFileSrc_TextChanged( object sender, EventArgs e )
        {
            string file_src = edFileSrc.Text.Trim();
            string file_dst = file_src;

            string fps = System.IO.Path.GetDirectoryName( file_src );
            string fns = System.IO.Path.GetFileNameWithoutExtension( file_src );
            string fes = System.IO.Path.GetExtension( file_src );

            file_dst = string.Format( "{0}\\{1}{2}{3}", fps, fns, suffix, fes );

            edFileDst.Text = file_dst.Trim();
        }

        private void rbToMars_CheckedChanged( object sender, EventArgs e )
        {
            if ( rbToMars.Checked )
            {
                suffix = "_mars";
            }
            else
            {
                suffix = "_wgs";
            }
        }

        private void rbToWGS_CheckedChanged( object sender, EventArgs e )
        {
            if ( rbToWGS.Checked )
            {
                suffix = "_wgs";
            }
            else
            {
                suffix = "_mars";
            }
        }

        private void edFileSrc_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            dlgOpen.DefaultExt = ".gpx";
            dlgOpen.Filter = "GPX File (*.gpx)|.gpx|KML File (*.kml)|.kml|All Files|*.*";
            dlgOpen.FilterIndex = 1;
            dlgOpen.FileName = "*.gpx";
            //dlgOpen.InitialDirectory = LastOpenFolder;
            if ( dlgOpen.ShowDialog() == DialogResult.OK )
            {
                edFileSrc.Text = dlgOpen.FileName.Trim();
                LastOpenFolder = System.IO.Path.GetDirectoryName( dlgOpen.FileName );
            }
        }

        private void edFileDst_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            dlgSave.DefaultExt = ".gpx";
            dlgSave.Filter = "GPX File (*.gpx)|.gpx|KML File (*.kml)|.kml|All Files|*.*";
            dlgSave.FilterIndex = 1;
            dlgSave.FileName = "*.gpx";
            //dlgSave.InitialDirectory = LastSaveFolder;
            if ( dlgSave.ShowDialog() == DialogResult.OK )
            {
                edFileDst.Text = dlgSave.FileName.Trim();
                LastSaveFolder = System.IO.Path.GetDirectoryName( dlgSave.FileName );
            }
        }

        private void btnExit_Click( object sender, EventArgs e )
        {
            Close();
        }

    }
}
