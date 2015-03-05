using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using System.IO;
using System.Text.RegularExpressions;

//[assembly: CompilationRelaxationsAttribute( CompilationRelaxations.NoStringInterning )]

namespace Mars2WGS
{
    enum MapSource { Google=0, Baidu=1 };
    enum ConvertingMode { LookTable=0, Formula=1 };
    enum ConvertingType { ToWGS=0, ToMars=1 };

    class GeoRegion
    {
        public double west;
        public double east;
        public double north;
        public double south;

        public GeoRegion(double w, double e, double n, double s)
        {
            west = w;
            east = e;
            north = n;
            south = s;
        }
    }

    class MarsWGS
    {
        private double[] TableX = new double[660 * 450];
        private double[] TableY = new double[660 * 450];

        private bool InitTable = false;

        string AppPath = System.AppDomain.CurrentDomain.BaseDirectory;

        //China region - raw data
        private List<GeoRegion> chinaRegion = new List<GeoRegion>();
        //China excluded region - raw data
        private  List<GeoRegion> excludeRegion = new List<GeoRegion>();

        private Boolean isInRect( GeoRegion rect, double lon, double lat )
        {
            return rect.west <= lon && rect.east >= lon && rect.north >= lat && rect.south <= lat;
        }

        private Boolean isInChina( double lon, double lat )
        {
            for ( int i = 0; i < chinaRegion.Count; i++ )
            {
                if ( isInRect( chinaRegion[i], lon, lat ) )
                {
                    for ( int j = 0; j < excludeRegion.Count; j++ )
                    {
                        if ( isInRect( excludeRegion[j], lon, lat ) )
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

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

        /// 最东端 东经135度2分30秒 黑龙江和乌苏里江交汇处
        /// 最西端 东经73度40分 帕米尔高原乌兹别里山口（乌恰县）
        /// 最南端 北纬3度52分 南沙群岛曾母暗沙
        /// 最北端 北纬53度33分 漠河以北黑龙江主航道（漠河)
        /// <summary>
        /// x是117左右，y是31左右
        /// </summary>
        /// <param name="xMars">中国地图纬度</param>
        /// <param name="yMars">中国地图经度</param>
        /// <param name="xWgs">GPS纬度</param>
        /// <param name="yWgs">GPS经度</param>
        public void Convert2WGS( double xMars, double yMars, out double xWgs, out double yWgs, ConvertingMode ConvertMethod=ConvertingMode.LookTable )
        {
            int i, j, k;
            double x1, y1, x2, y2, x3, y3, x4, y4, xtry, ytry, dx, dy;
            double t, u;

            xWgs = xMars;
            yWgs = yMars;

            if ( ( !InitTable ) || ( ConvertMethod == ConvertingMode.Formula ) )
            {
                xtry = xMars;
                ytry = yMars;
                Convert2Mars( xMars, yMars, out xtry, out ytry );
                dx = xtry - xMars;
                dy = ytry - yMars;

                xWgs = xMars - dx;
                yWgs = yMars - dy;
                return;
            }

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

        public void Convert2WGS( string tracklog_src, string tracklog_dst, ConvertingMode ConvertMethod = ConvertingMode.LookTable )
        {
            if(tracklog_src.EndsWith(".gpx", StringComparison.InvariantCultureIgnoreCase))
            {
                ConvertGPX( tracklog_src, tracklog_dst, true, ConvertMethod );
            }
            else if(tracklog_src.EndsWith(".kml", StringComparison.InvariantCultureIgnoreCase))
            {
                ConvertKML( tracklog_src, tracklog_dst, true, ConvertMethod );
            }
            else if ( tracklog_src.EndsWith( ".otrk2.xml", StringComparison.InvariantCultureIgnoreCase ) )
            {
                string dst = String.Format( "{0}.bak", tracklog_src );
                if ( File.Exists( dst ) )  File.Delete( dst );
                System.IO.File.Move( tracklog_src, dst );
                ConvertOtrk2( dst, tracklog_src, true, ConvertMethod );
            }
        }

        /// 最东端 东经135度2分30秒 黑龙江和乌苏里江交汇处
        /// 最西端 东经73度40分 帕米尔高原乌兹别里山口（乌恰县）
        /// 最南端 北纬3度52分 南沙群岛曾母暗沙
        /// 最北端 北纬53度33分 漠河以北黑龙江主航道（漠河)
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

            //if ( xWgs < 72.004 || xWgs > 137.8347 )
            //    return;
            //if ( yWgs < 9.9984 || yWgs > 55.8271 )
            //    return;
            if ( !isInChina( xWgs, yWgs ) ) return;

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

        public void Convert2Mars( string tracklog_src, string tracklog_dst )
        {
            if ( tracklog_src.EndsWith( ".gpx", StringComparison.InvariantCultureIgnoreCase ) )
            {
                ConvertGPX( tracklog_src, tracklog_dst, false );
            }
            else if ( tracklog_src.EndsWith( ".kml", StringComparison.InvariantCultureIgnoreCase ) )
            {
                ConvertKML( tracklog_src, tracklog_dst, false );
            }
            else if ( tracklog_src.EndsWith( ".otrk2.xml", StringComparison.InvariantCultureIgnoreCase ) )
            {
                string dst = String.Format( "{0}.bak", tracklog_src );
                if ( File.Exists( dst ) ) File.Delete( dst );
                System.IO.File.Move( tracklog_src, dst );
                ConvertOtrk2( dst, tracklog_src, false );
            }
        }

        private bool ConvertGPX( string FileSrc, string FileDst, bool ToWGS = true, ConvertingMode ConvertMethod = ConvertingMode.LookTable )
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
                            Convert2WGS( source_lon, source_lat, out target_lon, out target_lat, ConvertMethod );
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

        private bool ConvertKML( string FileSrc, string FileDst, bool ToWGS = true, ConvertingMode ConvertMethod = ConvertingMode.LookTable )
        {
            XmlDocument kml = new XmlDocument() { PreserveWhitespace = true };
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
                        string[] points = element.InnerText.Split( new Char[] { '\n', ' ', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                        StringBuilder target = new StringBuilder();

                        foreach ( string point in points )
                        {
                            string[] values = point.Trim().Split( new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
                            if ( values.Length == 3 )
                            {
                                source_lon = Convert.ToDouble( values[0] );
                                source_lat = Convert.ToDouble( values[1] );
                                source_ele = Convert.ToDouble( values[2] );
                            }
                            else
                            {
                                continue;
                            }
                            if ( ToWGS )
                            {
                                Convert2WGS( source_lon, source_lat, out target_lon, out target_lat, ConvertMethod );
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

        private bool ConvertOtrk2( string FileSrc, string FileDst, bool ToWGS = true, ConvertingMode ConvertMethod = ConvertingMode.LookTable )
        {
            XmlDocument otrk2 = new XmlDocument();
            XmlNodeList elements = null;

            double source_lat = 0;
            double source_lon = 0;
            double target_lat = 0;
            double target_lon = 0;

            if ( System.IO.File.Exists( FileSrc ) )
            {
                otrk2.Load( FileSrc );

                List<string> tagList = new List<string>();
                //tagList.Add( "MapCalibration" );
                tagList.Add( "CalibrationPoint" );

                #region convert points
                XmlNamespaceManager nsMan = new XmlNamespaceManager( otrk2.NameTable );
                nsMan.AddNamespace( "otrk2", "http://oruxtracker.com/app/res/calibration" );

                //elements = otrk2.GetElementsByTagName( "MapCalibration" );

                elements = otrk2.SelectNodes( "//otrk2:MapCalibration[@layers=\"false\"]", nsMan );
                foreach ( XmlNode element in elements )
                {
                    double min_lat=-180, min_lon=-90, max_lat=90, max_lon=90;
                    List<double> lons = new List<double>();
                    List<double> lats = new List<double>();

                    XmlNodeList CalibrationPoints = element.SelectNodes( "otrk2:CalibrationPoints/otrk2:CalibrationPoint", nsMan );
                    foreach ( XmlNode Point in CalibrationPoints )
                    {
                        XmlAttributeCollection attrs = Point.Attributes;
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
                            Convert2WGS( source_lon, source_lat, out target_lon, out target_lat, ConvertMethod );
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
                                lats.Add( target_lat );
                            }
                            if ( attr.Name.Equals( "lon", StringComparison.InvariantCultureIgnoreCase ) )
                            {
                                attr.Value = target_lon.ToString( "0.00000000" );
                                lons.Add( target_lon );
                            }
                        }
                    }
                    min_lat = lats.Min();
                    max_lat = lats.Max();
                    min_lon = lons.Min();
                    max_lon = lons.Max();

                    XmlNode MapBounds = element.SelectSingleNode( "otrk2:MapBounds", nsMan );
                    foreach ( XmlAttribute attr in MapBounds.Attributes )
                    {
                        if ( attr.Name.Equals( "minLat", StringComparison.InvariantCultureIgnoreCase ) )
                        {
                            attr.Value = min_lat.ToString( "0.000000000000000" );
                        }
                        if ( attr.Name.Equals( "maxLat", StringComparison.InvariantCultureIgnoreCase ) )
                        {
                            attr.Value = max_lat.ToString( "0.000000000000000" );
                        }
                        if ( attr.Name.Equals( "minLon", StringComparison.InvariantCultureIgnoreCase ) )
                        {
                            attr.Value = min_lon.ToString( "0.000000000000000" );
                        }
                        if ( attr.Name.Equals( "maxLon", StringComparison.InvariantCultureIgnoreCase ) )
                        {
                            attr.Value = max_lon.ToString( "0.000000000000000" );
                        }
                    }
                }
                #endregion
                otrk2.AppendChild( otrk2.CreateWhitespace( "\n" ) );
                otrk2.Save( FileDst );
            }
            return true;
        }

        public MarsWGS()
        {
            chinaRegion.Add( new GeoRegion( 79.446200, 49.220400, 96.330000, 42.889900 ) );
            chinaRegion.Add( new GeoRegion( 109.687200, 54.141500, 135.000200, 39.374200 ) );
            chinaRegion.Add( new GeoRegion( 73.124600, 42.889900, 124.143255, 29.529700 ) );
            chinaRegion.Add( new GeoRegion( 82.968400, 29.529700, 97.035200, 26.718600 ) );
            chinaRegion.Add( new GeoRegion( 97.025300, 29.529700, 124.367395, 20.414096 ) );
            chinaRegion.Add( new GeoRegion( 107.975793, 20.414096, 111.744104, 17.871542 ) );
            excludeRegion.Add( new GeoRegion( 119.921265, 25.398623, 122.497559, 21.785006 ) );
            excludeRegion.Add( new GeoRegion( 101.865200, 22.284000, 106.665000, 20.098800 ) );
            excludeRegion.Add( new GeoRegion( 106.452500, 21.542200, 108.051000, 20.487800 ) );
            excludeRegion.Add( new GeoRegion( 109.032300, 55.817500, 119.127000, 50.325700 ) );
            excludeRegion.Add( new GeoRegion( 127.456800, 55.817500, 137.022700, 49.557400 ) );
            excludeRegion.Add( new GeoRegion( 131.266200, 44.892200, 137.022700, 42.569200 ) );

            LoadText();
        }
    }
}
