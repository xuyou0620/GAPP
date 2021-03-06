﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPPSF.Core
{
    public partial class Settings
    {
        public string GetGeocacheNotes(string gcCode)
        {
            return _settingsStorage.GetGeocacheNotes(gcCode) ?? "";
        }
        public void SetGeocacheNotes(string gcCode, string notes)
        {
            _settingsStorage.SetGeocacheNotes(gcCode, notes);
        }

        public bool GCEditorEditActiveOnly
        {
            get { return bool.Parse(GetProperty("True")); }
            set { SetProperty(value.ToString()); }
        }

        public int GCEditorWindowWidth
        {
            get { return int.Parse(GetProperty("700")); }
            set { SetProperty(value.ToString()); }
        }
        public int GCEditorWindowHeight
        {
            get { return int.Parse(GetProperty("700")); }
            set { SetProperty(value.ToString()); }
        }
        public int GCEditorWindowTop
        {
            get { return int.Parse(GetProperty("100")); }
            set { SetProperty(value.ToString()); }
        }
        public int GCEditorWindowLeft
        {
            get { return int.Parse(GetProperty("100")); }
            set { SetProperty(value.ToString()); }
        }
    }
}
