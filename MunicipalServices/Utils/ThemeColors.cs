using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MunicipalServices.Utils
{
    public static class ThemeColors
    {
        // Primary colors
        public static Color Primary = Color.FromArgb(51, 122, 183);      // Steel Blue
        public static Color PrimaryDark = Color.FromArgb(41, 98, 146);   // Darker Steel Blue
        public static Color Secondary = Color.FromArgb(92, 184, 92);     // Green
        public static Color Accent = Color.FromArgb(0, 123, 255);        // Bright Blue
        
        // Background colors
        public static Color Background = Color.FromArgb(248, 249, 250);  // Light Gray
        public static Color CardBackground = Color.White;
        
        // Text colors
        public static Color TextPrimary = Color.FromArgb(33, 37, 41);    // Dark Gray
        public static Color TextSecondary = Color.FromArgb(108, 117, 125);// Medium Gray
        
        // Status colors
        public static Color Success = Color.FromArgb(40, 167, 69);       // Success Green
        public static Color Warning = Color.FromArgb(255, 193, 7);       // Warning Yellow
        public static Color Danger = Color.FromArgb(220, 53, 69);        // Error Red
        public static Color Border = Color.FromArgb(222, 226, 230);      // Light Border
    }
}
