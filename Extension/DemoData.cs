using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudDataManagement.Extension
{
    public class SourceData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public string Phone { get; set; }
    }

    public class TargetData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Manager { get; set; }
        public string Title { get; set; }
    }

    public class DemoData
    {
        public static List<SourceData> Sources = new List<SourceData>()
        {
            new SourceData(){ Id = 1, Name="tom", Address ="DL", Job ="DEV", Phone="111" },
            new SourceData(){ Id = 2, Name="jack", Address ="BJ", Job ="DEV1", Phone="112" },
            new SourceData(){ Id = 3, Name="rose", Address ="SH", Job ="DEV2", Phone="113" },
            new SourceData(){ Id = 5, Name="supper", Address ="GZ", Job ="DEV5", Phone="115" }
        };

        public static List<TargetData> Targets = new List<TargetData>()
        {
            new TargetData(){ Id = 1, Name="tom", Address ="DL", Job ="DEV", Phone="000", Age=20, Manager ="james", Title ="P8"},
            new TargetData(){ Id = 2, Name="jack", Address ="BJ", Job ="DEV1", Phone="112", Age=21, Manager ="james", Title ="P7"},
            new TargetData(){ Id = 3, Name="rose", Address ="SH", Job ="DEV2", Phone="113", Age=22, Manager ="james", Title ="P6"},
            new TargetData(){ Id = 4, Name="james", Address ="SZ", Job ="DEV3", Phone="114", Age=23, Manager ="james", Title ="P5"},
        };
    }

    public class WindowsAutopilotDeploymentProfileYamlModel
    {
        public string Id { get; set; }

        //public string ODataType { get; set; }

        public string Description { get; set; }

        public string DeviceNameTemplate { get; set; }

        public WindowsAutopilotDeviceType? DeviceType { get; set; }

        public string DisplayName { get; set; }

        public bool? EnableWhiteGlove { get; set; }

        public WindowsEnrollmentStatusScreenSettings EnrollmentStatusScreenSettings { get; set; }

        public bool? ExtractHardwareHash { get; set; }

        public string Language { get; set; }

        public OutOfBoxExperienceSettings OutOfBoxExperienceSettings { get; set; }
    }

    public class OutOfBoxExperienceSettings
    {
        public WindowsDeviceUsageType? DeviceUsageType { get; set; }
        public bool? HideEscapeLink { get; set; }
        public bool? HideEULA { get; set; }
        public bool? HidePrivacySettings { get; set; }
        public bool? SkipKeyboardSelectionPage { get; set; }
        public WindowsUserType? UserType { get; set; }
    }
}
