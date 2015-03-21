//Code written by: Andrew Wood
//Retrieved from: http://www.codeproject.com/Articles/22967/WPF-Runtime-Localization
//Used under  Code Project Open License (CPOL) license.

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Forms;


namespace ETD_Statistic.Services
{
    /// <summary>
    /// Wraps up XAML access to instance of WPFLocalize.Properties.Resources, list of available cultures, and method to change culture
    /// </summary>
    public class CultureResources
    {
        //only fetch installed cultures once
        private static bool bFoundInstalledCultures = false;

        private static Dictionary<String, CultureInfo> pSupportedCultures = new Dictionary<String, CultureInfo>();
        /// <summary>
        /// List of available cultures, enumerated at startup
        /// </summary>
        public static Dictionary<String, CultureInfo> SupportedCultures
        {
            get { return pSupportedCultures; }
        }
        public static Dictionary<String, CultureInfo>.KeyCollection SupportedCulturesNames
        {
            get { return pSupportedCultures.Keys; }
        }

        public CultureResources()
        {
            if (!bFoundInstalledCultures)
            {
                //determine which cultures are available to this application
                Debug.WriteLine("Get Installed cultures:");
                CultureInfo tCulture = new CultureInfo("");
                foreach (string dir in Directory.GetDirectories(Application.StartupPath))
                {
                    try
                    {
                        //see if this directory corresponds to a valid culture name
                        DirectoryInfo dirinfo = new DirectoryInfo(dir);
                        tCulture = CultureInfo.GetCultureInfo(dirinfo.Name);

                        //determine if a resources dll exists in this directory that matches the executable name
                        if (dirinfo.GetFiles(Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".resources.dll").Length > 0)
                        {
                            pSupportedCultures.Add(tCulture.NativeName, tCulture);
                            Debug.WriteLine(string.Format(" Found Culture: {0} [{1}]", tCulture.NativeName, tCulture.Name));
                        }
                    }
                    catch(ArgumentException) //ignore exceptions generated for any unrelated directories in the bin folder
                    {
                    }
                }
                bFoundInstalledCultures = true;
            }
        }

        /// <summary>
        /// The Resources ObjectDataProvider uses this method to get an instance of the WPFLocalize.Properties.Resources class
        /// </summary>
        /// <returns></returns>
        public Properties.Resources GetResourceInstance()
        {
            return new Properties.Resources();
        }

        private static ObjectDataProvider m_provider;
        public static ObjectDataProvider ResourceProvider
        {
            get
            {
                if (m_provider == null)
                    m_provider = (ObjectDataProvider)App.Current.FindResource("Resources");
                return m_provider;
            }
        }

        /// <summary>
        /// Change the current culture used in the application.
        /// If the desired culture is available all localized elements are updated.
        /// </summary>
        /// <param name="culture">Culture to change to</param>
        private static void ChangeCulture(CultureInfo culture)
        {   
            Properties.Resources.Culture = culture;
            ResourceProvider.Refresh();
        }

        /// <summary>
        /// Change the current culture used in the application.
        /// If the desired culture is available all localized elements are updated.
        /// </summary>
        /// <param name="culture">Culture to change to</param>
        public static void ChangeCulture(String culture)
        {
            //remain on the current culture if the desired culture cannot be found
            // - otherwise it would revert to the default resources set, which may or may not be desired.
            if (pSupportedCultures.ContainsKey(culture))
            {
                ChangeCulture(pSupportedCultures[culture]);
            }
            else
                Debug.WriteLine(string.Format("Culture [{0}] not available", culture));
        }
    }
}
