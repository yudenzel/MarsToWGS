using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using System.IO;
using System.Text.RegularExpressions;

namespace Mars2WGS
{
    class MarsWGS
    {
        private double[] TableX = new double[660 * 450];
        private double[] TableY = new double[660 * 450];

        private bool InitTable = false;

        string AppPath = System.AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="xMars">地图纬度</param>
        /// <param name="yMars">地图经度</param>
        /// <param name="xWgs">GPS纬度</param>
        /// <param name="yWgs">GPS经度</param>
        private void LoadText()
        {
            string OffsetFile = string.Format( "{0}{1}", AppPath, "Mars2Wgs.txt" );
            if ( System.IO.File.Exists( OffsetFile ) )
            {
                using ( StreamReader sr = new StreamReader( OffsetFile ) )
                {
                    string s = sr.ReadToEnd();

                    //textBox1.Text = s;

                    string[] lines = s.Split( '\n' );
                    Match MP = Regex.Match( s, "(\\d+)" );

                    int i = 0;
                    while ( MP.Success )
                    {
                        //MessageBox.Show(MP.Value);
                        if ( i % 2 == 0 ) //第一列和第三列
                        {
                            TableX[i / 2] = Convert.ToDouble( MP.Value ) / 100000.0;
                        }
                        else //第二列和第四列
                        {
                            TableY[( i - 1 ) / 2] = Convert.ToDouble( MP.Value ) / 100000.0;
                        }
                        i++;
                        MP = MP.NextMatch();
                    }
                    InitTable = true;
                    //MessageBox.Show((i / 2).ToString());
                }
            }
        }

        private int GetMarsID( int I, int J )
        {
            return I + 660 * J;
        }

        private int GetWGSID( int I, int J )
        {
            return ( I - 1 ) + 660 * ( J - 1 );
        }

        /// <summary>
        /// x是117左右，y是31左右
        /// </summary>
        /// <param name="xMars">中国地图纬度</param>
        /// <param name="yMars">中国地图经度</param>
        /// <param name="xWgs">GPS纬度</param>
        /// <param name="yWgs">GPS经度</param>
        public void Convert2WGS( double xMars, double yMars, out double xWgs, out double yWgs )
        {
            int i, j, k;
            double x1, y1, x2, y2, x3, y3, x4, y4, xtry, ytry, dx, dy;
            double t, u;

            xWgs = xMars;
            yWgs = yMars;

            if ( !InitTable )
                return;

            xtry = xMars;
            ytry = yMars;

            for ( k = 0; k < 10; ++k )
            {
                // 只对中国国境内数据转换
                if ( xtry < 72 || xtry > 137.9 || ytry < 10 || ytry > 54.9 )
                {
                    return;
                }

                i = (int)( ( xtry - 72.0 ) * 10.0 );
                j = (int)( ( ytry - 10.0 ) * 10.0 );

                x1 = TableX[GetMarsID( i, j )];
                y1 = TableY[GetMarsID( i, j )];
                x2 = TableX[GetMarsID( i + 1, j )];
                y2 = TableY[GetMarsID( i + 1, j )];
                x3 = TableX[GetMarsID( i + 1, j + 1 )];
                y3 = TableY[GetMarsID( i + 1, j + 1 )];
                x4 = TableX[GetMarsID( i, j + 1 )];
                y4 = TableY[GetMarsID( i, j + 1 )];

                t = ( xtry - 72.0 - 0.1 * i ) * 10.0;
                u = ( ytry - 10.0 - 0.1 * j ) * 10.0;

                dx = ( 1.0 - t ) * ( 1.0 - u ) * x1 + t * ( 1.0 - u ) * x2 + t * u * x3 + ( 1.0 - t ) * u * x4 - xtry;
                dy = ( 1.0 - t ) * ( 1.0 - u ) * y1 + t * ( 1.0 - u ) * y2 + t * u * y3 + ( 1.0 - t ) * u * y4 - ytry;

                xtry = ( xtry + xMars - dx ) / 2.0;
                ytry = ( ytry + yMars - dy ) / 2.0;
            }
            xWgs = xtry;
            yWgs = ytry;
        }

        public void Convert2WGS( string tracklog_src, string tracklog_dst )
        {
            string ext = System.IO.Path.GetExtension(tracklog_src);
            if ( string.Equals( ext, ".gpx", StringComparison.InvariantCultureIgnoreCase ) )
            {
                ConvertGPX( tracklog_src, tracklog_dst, true );
            }
            else if ( string.Equals( ext, ".kml", StringComparison.InvariantCultureIgnoreCase ) )
            {
                ConvertKML( tracklog_src, tracklog_dst, true );
            }
        }

        /// <summary>
        /// x是117左右，y是31左右
        /// </summary>
        /// <param name="xWgs">GPS纬度</param>
        /// <param name="yWgs">GPS经度</param>
        /// <param name="xMars">中国地图纬度</param>
        /// <param name="yMars">中国地图经度</param>
        public void Convert2Mars( double xWgs, double yWgs, out double xMars, out double yMars )
        {
            int i, j, k;
            double x1, y1, x2, y2, x3, y3, x4, y4, xtry, ytry, dx, dy;
            double t, u;

            xMars = xWgs;
            yMars = yWgs;

            if ( !InitTable )
                return;

            xtry = xWgs;
            ytry = yWgs;

            for ( k = 0; k < 10; ++k )
            {
                // 只对中国国境内数据转换
                if ( xtry < 72 || xtry > 137.9 || ytry < 10 || ytry > 54.9 )
                {
                    return;
                }

                i = (int)( ( xtry - 72.0 ) * 10.0 );
                j = (int)( ( ytry - 10.0 ) * 10.0 );

                x1 = TableX[GetWGSID( i, j )];
                y1 = TableY[GetWGSID( i, j )];
                x2 = TableX[GetWGSID( i - 1, j )];
                y2 = TableY[GetWGSID( i - 1, j )];
                x3 = TableX[GetWGSID( i - 1, j - 1 )];
                y3 = TableY[GetWGSID( i - 1, j - 1 )];
                x4 = TableX[GetWGSID( i, j - 1 )];
                y4 = TableY[GetWGSID( i, j - 1 )];

                t = ( xtry - 72.0 - 0.1 * i ) * 10.0;
                u = ( ytry - 10.0 - 0.1 * j ) * 10.0;

                dx = ( 1.0 - t ) * ( 1.0 - u ) * x1 + t * ( 1.0 - u ) * x2 + t * u * x3 + ( 1.0 - t ) * u * x4 - xtry;
                dy = ( 1.0 - t ) * ( 1.0 - u ) * y1 + t * ( 1.0 - u ) * y2 + t * u * y3 + ( 1.0 - t ) * u * y4 - ytry;

                xtry = ( xtry + xWgs - dx ) / 2.0;
                ytry = ( ytry + yWgs - dy ) / 2.0;
            }
            xMars = xtry;
            yMars = ytry;
        }

        public void Convert2Mars(string tracklog_src, string tracklog_dst)
        {
            string ext = System.IO.Path.GetExtension( tracklog_src );
            if ( string.Equals( ext, ".gpx", StringComparison.InvariantCultureIgnoreCase ) )
            {
                ConvertGPX( tracklog_src, tracklog_dst, false );
            }
            else if ( string.Equals( ext, ".kml", StringComparison.InvariantCultureIgnoreCase ) )
            {
                ConvertKML( tracklog_src, tracklog_dst, false );
            }
        }

        private bool ConvertGPX( string FileSrc, string FileDst, bool ToWGS = true )
        {
            XmlDocument gpx = new XmlDocument();
            XmlNodeList elements = null;

            double source_lat = 0;
            double source_lon = 0;
            double target_lat = 0;
            double target_lon = 0;

            if ( System.IO.File.Exists( FileSrc ) )
            {
                gpx.Load( FileSrc );

                List<string> tagList = new List<string>();
                tagList.Add( "wpt" );
                tagList.Add( "trkpt" );
                tagList.Add( "rtept" );

                #region convert points
                foreach ( string tag in tagList )
                {
                    elements = gpx.GetElementsByTagName( tag );
                    foreach ( XmlNode element in elements )
                    {
                        XmlAttributeCollection attrs = element.Attributes;
                        foreach ( XmlAttribute attr in attrs )
                        {
                            if ( attr.Name.Equals( "lat", StringComparison.InvariantCultureIgnoreCase ) )
                            {
                                source_lat = Convert.ToDouble( attr.Value );
                            }
                            if ( attr.Name.Equals( "lon", StringComparison.InvariantCultureIgnoreCase ) )
                            {
                                source_lon = Convert.ToDouble( attr.Value );
                            }
                        }
                        if ( ToWGS )
                        {
                            Convert2WGS( source_lon, source_lat, out target_lon, out target_lat );
                        }
                        else
                        {
                            Convert2Mars( source_lon, source_lat, out target_lon, out target_lat );
                        }
                        foreach ( XmlAttribute attr in attrs )
                        {
                            if ( attr.Name.Equals( "lat", StringComparison.InvariantCultureIgnoreCase ) )
                            {
                                attr.Value = target_lat.ToString( "0.00000000" );
                            }
                            if ( attr.Name.Equals( "lon", StringComparison.InvariantCultureIgnoreCase ) )
                            {
                                attr.Value = target_lon.ToString( "0.00000000" );
                            }
                        }
                    }
                }
                #endregion
                gpx.Save( FileDst );
            }
            return true;
        }

        private bool ConvertKML( string FileSrc, string FileDst, bool ToWGS = true )
        {
            XmlDocument kml = new XmlDocument();
            XmlNodeList elements = null;

            double source_lat = 0;
            double source_lon = 0;
            double source_ele = 0;
            double target_lat = 0;
            double target_lon = 0;
            double target_ele = 0;

            if ( System.IO.File.Exists( FileSrc ) )
            {
                kml.Load( FileSrc );

                List<string> tagList = new List<string>();
                tagList.Add( "coordinates" );

                #region convert points
                foreach ( string tag in tagList )
                {
                    elements = kml.GetElementsByTagName( tag );
                    foreach ( XmlNode element in elements )
                    {
                        string[] points = element.InnerText.Split( new Char[] { '\n', ' ', '\r' });
                        StringBuilder target = new StringBuilder();

                        foreach ( string point in points )
                        {
                            string[] values = point.Trim().Split( new Char[] { ',' } );
                            if ( values.Length == 3 )
                            {
                                source_lon = Convert.ToDouble( values[0] );
                                source_lat = Convert.ToDouble( values[1] );
                                source_ele = Convert.ToDouble( values[2] );
                            }
                            if ( ToWGS )
                            {
                                Convert2WGS( source_lon, source_lat, out target_lon, out target_lat );
                            }
                            else
                            {
                                Convert2Mars( source_lon, source_lat, out target_lon, out target_lat );
                            }
                            target_ele = source_ele;
                            target.AppendLine( string.Format( "{0:F6},{1:F6},{2:F6}", target_lon, target_lat, target_ele ) );
                        }
                        element.InnerText = target.ToString().Trim();
                    }
                }
                #endregion
                kml.Save( FileDst );
            }
            return true;
        }

        public MarsWGS()
        {
            LoadText();
        }
    }
}
