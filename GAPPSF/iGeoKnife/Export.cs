﻿using Community.CsharpSqlite.SQLiteClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPPSF.iGeoKnife
{
    public class Export
    {
        public static void ExportToFile(string filename, List<Core.Data.Geocache> gcList)
        {
            try
            {
                DateTime nextUpdate = DateTime.Now.AddSeconds(1);
                using (Utils.ProgressBlock fixscr = new Utils.ProgressBlock("ExportingiGeoKnife", "CreatingFile", 1, 0, true))
                {
                    System.Collections.Hashtable logTypes = new System.Collections.Hashtable();
                    logTypes.Add(2, "Found it");
                    logTypes.Add(3, "Didn't find it");
                    logTypes.Add(4, "Write note");
                    logTypes.Add(5, "Archive");
                    logTypes.Add(7, "Needs Archived");
                    logTypes.Add(9, "Will Attend");
                    logTypes.Add(10, "Attended");
                    logTypes.Add(11, "Webcam Photo Taken");
                    logTypes.Add(12, "Unarchive");
                    logTypes.Add(22, "Temporarily Disable Listing");
                    logTypes.Add(23, "Enable Listing");
                    logTypes.Add(24, "Publish Listing");
                    logTypes.Add(25, "Retract Listing");
                    logTypes.Add(45, "Needs Maintenance");
                    logTypes.Add(46, "Owner Maintenance");
                    logTypes.Add(47, "Update Coordinates");
                    logTypes.Add(68, "Post Reviewer Note");
                    logTypes.Add(74, "Announcement");

                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }

                    Utils.ResourceHelper.SaveToFile("/iGeoKnife/sqlite.db3", filename, true);

                    using (SqliteConnection dbcon = new SqliteConnection(string.Format("data source=file:{0}", filename)))
                    {
                        dbcon.Open();

                        DbParameter par;
                        using (Utils.ProgressBlock progress = new Utils.ProgressBlock("ExportingiGeoKnife", "SavingGeocaches", gcList.Count, 0, true))
                        {
                            using (SqliteCommand cmd = new SqliteCommand("", dbcon))
                            using (SqliteCommand cmd2 = new SqliteCommand("", dbcon))
                            using (SqliteCommand cmd3 = new SqliteCommand("", dbcon))
                            using (SqliteCommand cmd4 = new SqliteCommand("", dbcon))
                            using (SqliteCommand cmd5 = new SqliteCommand("", dbcon))
                            using (SqliteCommand cmd6 = new SqliteCommand("", dbcon))
                            using (SqliteCommand cmd7 = new SqliteCommand("", dbcon))
                            using (SqliteCommand cmd8 = new SqliteCommand("", dbcon))
                            {
                                cmd.CommandText = "drop index CachesSmart";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "insert into Caches (Code, Name, PlacedBy, Archived, CacheId, CacheType, Container, Country, Difficulty, Found, HasCorrected, HasUserNote, Latitude, LongHtm, Longitude, OwnerName, PlacedDate, ShortHtm, State, Terrain, UserFlag, IsOwner, LatOriginal, LonOriginal, Status, GcNote, IsPremium, FavPoints) values (@Code, @Name, @PlacedBy, @Archived, @CacheId, @CacheType, @Container, @Country, @Difficulty, @Found, @HasCorrected, @HasUserNote, @Latitude, @LongHtm, @Longitude, @OwnerName, @PlacedDate, @ShortHtm, @State, @Terrain, @UserFlag, @IsOwner, @LatOriginal, @LonOriginal, @Status, @GcNote, @IsPremium, @FavPoints)";
                                cmd2.CommandText = "insert into CacheMemo (Code, LongDescription, ShortDescription, Url, Hints, UserNote) values (@Code, @LongDescription, @ShortDescription, @Url, @Hints, @UserNote)";
                                cmd3.CommandText = "insert into Attributes (aCode, aId, aInc) values (@aCode, @aId, @aInc)";
                                cmd4.CommandText = "insert into LogMemo (lParent, lLogId, lText) values (@lParent, @lLogId, @lText)";
                                cmd5.CommandText = "insert into Logs (lParent, lLogId, lType, lBy, lDate, lLat, lLon, lEncoded, lownerid, lHasHtml, lIsowner, lTime) values (@lParent, @lLogId, @lType, @lBy, @lDate, @lLat, @lLon, @lEncoded, @lownerid, @lHasHtml, @lIsowner, @lTime)";
                                cmd6.CommandText = "insert into WayMemo (cParent, cCode, cComment, cUrl) values (@cParent, @cCode, @cComment, @cUrl)";
                                cmd7.CommandText = "insert into Waypoints (cParent, cCode, cPrefix, cName, cType, cLat, cLon, cByuser, cDate, cFlag, sB1) values (@cParent, @cCode, @cPrefix, @cName, @cType, @cLat, @cLon, @cByuser, @cDate, @cFlag, @sB1)";
                                cmd8.CommandText = "insert into Corrected (kCode, kBeforeLat, kBeforeLon, kAfterLat, kAfterLon) values (@kCode, @kBeforeLat, @kBeforeLon, @kAfterLat, @kAfterLon)";

                                par = cmd8.CreateParameter();
                                par.ParameterName = "@kCode";
                                par.DbType = DbType.String;
                                cmd8.Parameters.Add(par);
                                par = cmd8.CreateParameter();
                                par.ParameterName = "@kBeforeLat";
                                par.DbType = DbType.String;
                                cmd8.Parameters.Add(par);
                                par = cmd8.CreateParameter();
                                par.ParameterName = "@kBeforeLon";
                                par.DbType = DbType.String;
                                cmd8.Parameters.Add(par);
                                par = cmd8.CreateParameter();
                                par.ParameterName = "@kAfterLat";
                                par.DbType = DbType.String;
                                cmd8.Parameters.Add(par);
                                par = cmd8.CreateParameter();
                                par.ParameterName = "@kAfterLon";
                                par.DbType = DbType.String;
                                cmd8.Parameters.Add(par);

                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cParent";
                                par.DbType = DbType.String;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cCode";
                                par.DbType = DbType.String;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cPrefix";
                                par.DbType = DbType.String;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cName";
                                par.DbType = DbType.String;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cType";
                                par.DbType = DbType.String;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cLat";
                                par.DbType = DbType.String;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cLon";
                                par.DbType = DbType.String;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cByuser";
                                par.DbType = DbType.Boolean;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cDate";
                                par.DbType = DbType.String;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@cFlag";
                                par.DbType = DbType.Boolean;
                                cmd7.Parameters.Add(par);
                                par = cmd7.CreateParameter();
                                par.ParameterName = "@sB1";
                                par.DbType = DbType.Boolean;
                                cmd7.Parameters.Add(par);

                                par = cmd6.CreateParameter();
                                par.ParameterName = "@cParent";
                                par.DbType = DbType.String;
                                cmd6.Parameters.Add(par);
                                par = cmd6.CreateParameter();
                                par.ParameterName = "@cCode";
                                par.DbType = DbType.String;
                                cmd6.Parameters.Add(par);
                                par = cmd6.CreateParameter();
                                par.ParameterName = "@cComment";
                                par.DbType = DbType.String;
                                cmd6.Parameters.Add(par);
                                par = cmd6.CreateParameter();
                                par.ParameterName = "@cUrl";
                                par.DbType = DbType.String;
                                cmd6.Parameters.Add(par);

                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lParent";
                                par.DbType = DbType.String;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lLogId";
                                par.DbType = DbType.Int32;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lType";
                                par.DbType = DbType.String;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lBy";
                                par.DbType = DbType.String;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lDate";
                                par.DbType = DbType.String;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lLat";
                                par.DbType = DbType.String;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lLon";
                                par.DbType = DbType.String;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lEncoded";
                                par.DbType = DbType.Boolean;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lownerid";
                                par.DbType = DbType.Int32;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lHasHtml";
                                par.DbType = DbType.Boolean;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lIsowner";
                                par.DbType = DbType.Boolean;
                                cmd5.Parameters.Add(par);
                                par = cmd5.CreateParameter();
                                par.ParameterName = "@lTime";
                                par.DbType = DbType.String;
                                cmd5.Parameters.Add(par);

                                par = cmd4.CreateParameter();
                                par.ParameterName = "@lParent";
                                par.DbType = DbType.String;
                                cmd4.Parameters.Add(par);
                                par = cmd4.CreateParameter();
                                par.ParameterName = "@lLogId";
                                par.DbType = DbType.Int32;
                                cmd4.Parameters.Add(par);
                                par = cmd4.CreateParameter();
                                par.ParameterName = "@lText";
                                par.DbType = DbType.String;
                                cmd4.Parameters.Add(par);

                                par = cmd3.CreateParameter();
                                par.ParameterName = "@aCode";
                                par.DbType = DbType.String;
                                cmd3.Parameters.Add(par);
                                par = cmd3.CreateParameter();
                                par.ParameterName = "@aId";
                                par.DbType = DbType.Int32;
                                cmd3.Parameters.Add(par);
                                par = cmd3.CreateParameter();
                                par.ParameterName = "@aInc";
                                par.DbType = DbType.Int32;
                                cmd3.Parameters.Add(par);

                                par = cmd2.CreateParameter();
                                par.ParameterName = "@Code";
                                par.DbType = DbType.String;
                                cmd2.Parameters.Add(par);
                                par = cmd2.CreateParameter();
                                par.ParameterName = "@LongDescription";
                                par.DbType = DbType.String;
                                cmd2.Parameters.Add(par);
                                par = cmd2.CreateParameter();
                                par.ParameterName = "@ShortDescription";
                                par.DbType = DbType.String;
                                cmd2.Parameters.Add(par);
                                par = cmd2.CreateParameter();
                                par.ParameterName = "@Url";
                                par.DbType = DbType.String;
                                cmd2.Parameters.Add(par);
                                par = cmd2.CreateParameter();
                                par.ParameterName = "@Hints";
                                par.DbType = DbType.String;
                                cmd2.Parameters.Add(par);
                                par = cmd2.CreateParameter();
                                par.ParameterName = "@UserNote";
                                par.DbType = DbType.String;
                                cmd2.Parameters.Add(par);

                                par = cmd.CreateParameter();
                                par.ParameterName = "@Code";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Name";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@PlacedBy";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Archived";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@CacheId";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@CacheType";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Container";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Country";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Difficulty";
                                par.DbType = DbType.Double;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Found";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@HasCorrected";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@HasUserNote";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Latitude";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@LongHtm";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Longitude";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@OwnerName";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@PlacedDate";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@ShortHtm";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@State";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Terrain";
                                par.DbType = DbType.Double;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@UserFlag";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@IsOwner";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@LatOriginal";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@LonOriginal";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@Status";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@GcNote";
                                par.DbType = DbType.String;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@IsPremium";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);
                                par = cmd.CreateParameter();
                                par.ParameterName = "@FavPoints";
                                par.DbType = DbType.Int32;
                                cmd.Parameters.Add(par);

                                cmd.Prepare();
                                cmd2.Prepare();
                                cmd3.Prepare();
                                cmd4.Prepare();
                                cmd5.Prepare();
                                cmd6.Prepare();
                                cmd7.Prepare();
                                cmd8.Prepare();

                                //using (DbTransaction trans = dbcon.BeginTransaction())
                                //{
                                int index = 0;
                                foreach (Core.Data.Geocache gc in gcList)
                                {
                                    string notes = "";
                                    if (!string.IsNullOrEmpty(gc.Notes))
                                    {
                                        notes = System.Web.HttpUtility.HtmlDecode(gc.Notes);
                                    }
                                    if (!string.IsNullOrEmpty(gc.PersonalNote))
                                    {
                                        notes = string.Concat(notes, gc.PersonalNote);
                                    }

                                    cmd2.Parameters["@Code"].Value = gc.Code;
                                    cmd2.Parameters["@LongDescription"].Value = gc.LongDescription ?? "";
                                    cmd2.Parameters["@ShortDescription"].Value = gc.ShortDescription ?? "";
                                    cmd2.Parameters["@Url"].Value = gc.Url ?? "";
                                    cmd2.Parameters["@Hints"].Value = gc.EncodedHints ?? "";
                                    cmd2.Parameters["@UserNote"].Value = notes;

                                    cmd.Parameters["@Code"].Value = gc.Code;
                                    cmd.Parameters["@Name"].Value = gc.Name ?? "";
                                    cmd.Parameters["@PlacedBy"].Value = gc.PlacedBy ?? "";
                                    cmd.Parameters["@Archived"].Value = gc.Archived ? 1 : 0;
                                    cmd.Parameters["@CacheId"].Value = Utils.Conversion.GetCacheIDFromCacheCode(gc.Code).ToString();
                                    cmd.Parameters["@CacheType"].Value = getCacheType(gc.GeocacheType);
                                    cmd.Parameters["@Container"].Value = getContainer(gc.Container);
                                    cmd.Parameters["@Country"].Value = gc.Country ?? "";
                                    cmd.Parameters["@Difficulty"].Value = gc.Difficulty;
                                    cmd.Parameters["@Found"].Value = gc.Found ? 1 : 0;
                                    cmd.Parameters["@HasCorrected"].Value = gc.ContainsCustomLatLon ? 1 : 0;
                                    cmd.Parameters["@HasUserNote"].Value = gc.ContainsNote ? 1 : 0;
                                    cmd.Parameters["@LatOriginal"].Value = gc.Lat.ToString().Replace(',', '.');
                                    cmd.Parameters["@LonOriginal"].Value = gc.Lon.ToString().Replace(',', '.');
                                    if (gc.ContainsCustomLatLon)
                                    {
                                        cmd.Parameters["@Latitude"].Value = gc.CustomLat.ToString().Replace(',', '.');
                                        cmd.Parameters["@Longitude"].Value = gc.CustomLon.ToString().Replace(',', '.');
                                    }
                                    else
                                    {
                                        cmd.Parameters["@Latitude"].Value = gc.Lat.ToString().Replace(',', '.');
                                        cmd.Parameters["@Longitude"].Value = gc.Lon.ToString().Replace(',', '.');
                                    }
                                    cmd.Parameters["@LongHtm"].Value = gc.LongDescriptionInHtml ? 1 : 0;
                                    cmd.Parameters["@OwnerName"].Value = gc.Owner ?? "";
                                    cmd.Parameters["@PlacedDate"].Value = gc.PublishedTime.ToString("yyyy-MM-dd");
                                    cmd.Parameters["@ShortHtm"].Value = gc.ShortDescriptionInHtml ? 1 : 0;
                                    cmd.Parameters["@State"].Value = gc.State ?? "";
                                    cmd.Parameters["@Terrain"].Value = gc.Terrain;
                                    cmd.Parameters["@UserFlag"].Value = gc.Flagged ? 1 : 0;
                                    cmd.Parameters["@IsOwner"].Value = gc.IsOwn ? 1 : 0;
                                    cmd.Parameters["@Status"].Value = gc.Available ? "A" : gc.Archived ? "X" : "T";
                                    cmd.Parameters["@GcNote"].Value = notes;
                                    cmd.Parameters["@IsPremium"].Value = gc.MemberOnly ? 1 : 0;
                                    cmd.Parameters["@FavPoints"].Value = gc.Favorites;

                                    cmd.ExecuteNonQuery();
                                    cmd2.ExecuteNonQuery();

                                    if (gc.ContainsCustomLatLon)
                                    {
                                        cmd8.Parameters["@kCode"].Value = gc.Code;
                                        cmd8.Parameters["@kBeforeLat"].Value = gc.Lat.ToString().Replace(',', '.');
                                        cmd8.Parameters["@kBeforeLon"].Value = gc.Lon.ToString().Replace(',', '.');
                                        cmd8.Parameters["@kAfterLat"].Value = gc.CustomLat.ToString().Replace(',', '.');
                                        cmd8.Parameters["@kAfterLon"].Value = gc.CustomLon.ToString().Replace(',', '.');

                                        cmd8.ExecuteNonQuery();
                                    }

                                    List<int> attr = gc.AttributeIds;
                                    foreach (int att in attr)
                                    {
                                        cmd3.Parameters["@aCode"].Value = gc.Code;
                                        cmd3.Parameters["@aId"].Value = Math.Abs(att);
                                        cmd3.Parameters["@aInc"].Value = att < 0 ? 0 : 1;

                                        cmd3.ExecuteNonQuery();
                                    }

                                    List<Core.Data.Log> logs = Utils.DataAccess.GetLogs(gc.Database, gc.Code).Take(Core.Settings.Default.IGeoKnifeMaxLogs).ToList();
                                    foreach (Core.Data.Log l in logs)
                                    {
                                        try
                                        {
                                            int logid = 0;
                                            if (!int.TryParse(l.ID, out logid))
                                            {
                                                logid = Utils.Conversion.GetCacheIDFromCacheCode(l.ID);
                                            }
                                            cmd4.Parameters["@lLogId"].Value = logid;
                                            cmd4.Parameters["@lText"].Value = l.Text ?? "";
                                            cmd4.Parameters["@lParent"].Value = gc.Code;
                                            cmd4.ExecuteNonQuery();

                                            cmd5.Parameters["@lLogId"].Value = logid;
                                            cmd5.Parameters["@lParent"].Value = gc.Code;
                                            object o = logTypes[l.LogType.ID];
                                            if (o == null)
                                            {
                                                cmd5.Parameters["@lType"].Value = 4;
                                            }
                                            else
                                            {
                                                cmd5.Parameters["@lType"].Value = (string)o;
                                            }
                                            cmd5.Parameters["@lBy"].Value = l.Finder ?? "";
                                            cmd5.Parameters["@lDate"].Value = l.Date.ToString("yyyy-MM-dd HH:mm:ss");
                                            cmd5.Parameters["@lLat"].Value = DBNull.Value;
                                            cmd5.Parameters["@lLon"].Value = DBNull.Value;
                                            cmd5.Parameters["@lEncoded"].Value = l.Encoded;
                                            try
                                            {
                                                cmd5.Parameters["@lownerid"].Value = int.Parse(l.FinderId);
                                            }
                                            catch
                                            {
                                            }
                                            cmd5.Parameters["@lHasHtml"].Value = false;
                                            cmd5.Parameters["@lIsowner"].Value = gc.IsOwn;
                                            cmd5.Parameters["@lTime"].Value = "";

                                            cmd5.ExecuteNonQuery();

                                        }
                                        catch(Exception e)
                                        {
                                            Core.ApplicationData.Instance.Logger.AddLog(new Export(), e);
                                        }
                                    }

                                    List<Core.Data.Waypoint> wps = Utils.DataAccess.GetWaypointsFromGeocache(gc.Database, gc.Code);
                                    foreach (Core.Data.Waypoint w in wps)
                                    {
                                        try
                                        {
                                            cmd6.Parameters["@cParent"].Value = gc.Code;
                                            cmd6.Parameters["@cCode"].Value = w.Code;
                                            cmd6.Parameters["@cComment"].Value = w.Comment;
                                            cmd6.Parameters["@cUrl"].Value = w.Url;

                                            cmd7.Parameters["@cParent"].Value = gc.Code;
                                            cmd7.Parameters["@cCode"].Value = w.Code;
                                            cmd7.Parameters["@cPrefix"].Value = w.Code.Substring(0, 2);
                                            cmd7.Parameters["@cName"].Value = w.Name ?? "";
                                            cmd7.Parameters["@cType"].Value = getWPType(w.WPType);
                                            cmd7.Parameters["@cLat"].Value = w.Lat == null ? "0.0" : w.Lat.ToString().Replace(',', '.');
                                            cmd7.Parameters["@cLon"].Value = w.Lon == null ? "0.0" : w.Lon.ToString().Replace(',', '.');
                                            cmd7.Parameters["@cByuser"].Value = false;
                                            cmd7.Parameters["@cDate"].Value = w.Time.ToString("yyyy-MM-dd");
                                            cmd7.Parameters["@cFlag"].Value = false;
                                            cmd7.Parameters["@sB1"].Value = false;

                                            cmd7.ExecuteNonQuery();
                                            cmd6.ExecuteNonQuery();
                                        }
                                        catch(Exception e)
                                        {
                                            Core.ApplicationData.Instance.Logger.AddLog(new Export(), e);
                                        }
                                    }

                                    index++;
                                    if (DateTime.Now>=nextUpdate)
                                    {
                                        if (!progress.Update("SavingGeocaches", gcList.Count, index))
                                        {
                                            break;
                                        }
                                        nextUpdate = DateTime.Now.AddSeconds(1);
                                    }
                                }
                                //trans.Commit();
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Core.ApplicationData.Instance.Logger.AddLog(new Export(), e);
            }
        }

        private static string getWPType(Core.Data.WaypointType type)
        {
            switch (type.ID)
            {
                case 217: return "Parking Area";
                case 220: return "Final location";
                case 218: return "Question to Answer";
                case 452: return "Reference Point";
                case 219: return "Stages of a Multicache";
                case 221: return "Trailhead";
                default: return "Stages of a Multicache";
            }
        }

        private static string getContainer(Core.Data.GeocacheContainer container)
        {
            switch (container.ID)
            {
                case 1: return "Not chosen";
                case 2: return "Micro";
                case 3: return "Regular";
                case 4: return "Large";
                case 5: return "Virtual";
                case 6: return "Other";
                case 8: return "Small";
                case 9: return "A";
                default: return "Other";
            }
        }

        private static string getCacheType(Core.Data.GeocacheType type)
        {
            switch (type.ID)
            {
                case 2: return "T";
                case 3: return "M";
                case 4: return "V";
                case 5: return "B";
                case 6: return "E";
                case 8: return "U";
                case 9: return "A";
                case 11: return "W";
                case 12: return "L";
                case 13: return "C";
                case 137: return "R";
                case 453: return "Z";
                case 1304: return "X";
                case 1858: return "I";
                case 3653: return "F";
                case 3773: return "H";
                case 3774: return "D";
                default: return "O";
            }
        }
    }
}
