﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using System.IO;
using System.Text.RegularExpressions;

//[assembly: CompilationRelaxationsAttribute( CompilationRelaxations.NoStringInterning )] 

namespace Mars2WGS
{
    enum MapSource { Google=0, Baidu=1 };
    enum ConvertingMode { LookTable=0, Formula=1 };
    enum ConvertingType { ToWGS=0, ToMars=1 };

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
                    s = null;
                    sr.Close();
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
            xMars = xWgs;
            yMars = yWgs;

            const double pi = 3.14159265358979324;

            //
            // Krasovsky 1940
            //
            // a = 6378245.0, 1/f = 298.3
            // b = a * (1 - f)
            // ee = (a^2 - b^2) / a^2;
            const double a = 6378245.0;
            const double ee = 0.00669342162296594323;

            if ( xWgs < 72.004 || xWgs > 137.8347 )
                return;
            if ( yWgs < 0.8293 || yWgs > 55.8271 )
                return;

            double x=0, y=0;
            x = xWgs - 105.0;
            y = yWgs - 35.0;

            double dLon =  300.0 + 1.0 * x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt( Math.Abs( x ) );
            dLon += (  20.0 * Math.Sin( 6.0 * x * pi ) + 20.0 * Math.Sin( 2.0 * x * pi ) ) * 2.0 / 3.0;
            dLon += (  20.0 * Math.Sin( x * pi ) + 40.0 * Math.Sin(x / 3.0 * pi ) ) * 2.0 / 3.0;
            dLon += ( 150.0 * Math.Sin( x / 12.0 * pi ) + 300.0 * Math.Sin( x / 30.0 * pi ) ) * 2.0 / 3.0;            
            
            double dLat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt( Math.Abs( x ) );
            dLat += (  20.0 * Math.Sin( 6.0 * x * pi ) + 20.0 * Math.Sin( 2.0 * x * pi ) ) * 2.0 / 3.0;
            dLat += (  20.0 * Math.Sin( y * pi ) + 40.0 * Math.Sin( y / 3.0 * pi ) ) * 2.0 / 3.0;
            dLat += ( 160.0 * Math.Sin( y / 12.0 * pi ) + 320.0 * Math.Sin( y * pi / 30.0 ) ) * 2.0 / 3.0;

            double radLat = yWgs / 180.0 * pi;
            double magic = Math.Sin( radLat );
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt( magic );
            dLon = ( dLon * 180.0 ) / ( a / sqrtMagic * Math.Cos( radLat ) * pi );
            dLat = ( dLat * 180.0 ) / ( ( a * ( 1 - ee ) ) / ( magic * sqrtMagic ) * pi );
            xMars = xWgs + dLon;
            yMars = yWgs + dLat;
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
                gpx.AppendChild( gpx.CreateWhitespace("\n") );
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
                kml.AppendChild( kml.CreateWhitespace( "\n" ) );
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
