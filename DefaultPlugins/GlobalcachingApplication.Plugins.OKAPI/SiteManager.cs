﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalcachingApplication.Plugins.OKAPI
{
    public class SiteManager
    {
        public const string STR_NOSITESELECTED = "Please select an opencaching site first by using the settings.";
        public const string STR_ERROR = "Error";
        public const string STR_MUSTGETUSERID = "You must obtain a User ID in order to execute this plugin. Please check your settings.";

        private static SiteManager _uniqueInstance = null;
        private static object _lockObject = new object();

        public List<SiteInfo> AvailableSites { get; private set; }
        public SiteInfo _activeSite = null;

        private SiteManager()
        {
            AvailableSites = new List<SiteInfo>();
            AvailableSites.Add(new SiteInfoGermany());
            AvailableSites.Add(new SiteInfoNetherlands());
            AvailableSites.Add(new SiteInfoPoland());
            //AvailableSites.Add(new SiteInfoUSA());
            //AvailableSites.Add(new SiteInfoUK());

            foreach (SiteInfo si in AvailableSites)
            {
                si.LoadSettings();
            }
            _activeSite = (from a in AvailableSites where a.ID == PluginSettings.Instance.ActiveSiteID select a).FirstOrDefault();
        }

        public static SiteManager Instance
        {
            get
            {
                if (_uniqueInstance == null)
                {
                    lock (_lockObject)
                    {
                        if (_uniqueInstance == null)
                        {
                            _uniqueInstance = new SiteManager();
                        }
                    }
                }
                return _uniqueInstance;
            }
        }

        public SiteInfo ActiveSite
        {
            get { return _activeSite; }
            set
            {
                _activeSite = value;
                if (_activeSite != null)
                {
                    PluginSettings.Instance.ActiveSiteID = _activeSite.ID;
                }
                else
                {
                    PluginSettings.Instance.ActiveSiteID = "";
                }
            }
        }

        public bool CheckAPIAccess()
        {
            bool result = false;
            if (ActiveSite == null)
            {
                System.Windows.Forms.MessageBox.Show(Utils.LanguageSupport.Instance.GetTranslation(STR_NOSITESELECTED), Utils.LanguageSupport.Instance.GetTranslation(STR_ERROR), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(ActiveSite.UserID))
            {
                System.Windows.Forms.MessageBox.Show(Utils.LanguageSupport.Instance.GetTranslation(STR_MUSTGETUSERID), Utils.LanguageSupport.Instance.GetTranslation(STR_ERROR), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
