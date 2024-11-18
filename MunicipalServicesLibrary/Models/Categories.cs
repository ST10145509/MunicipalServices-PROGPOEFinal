using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.Models
{
    public enum IssueCategory
    {
        RoadMaintenance,
        StreetLighting,
        WaterSupply,
        Sewage,
        GarbageCollection,
        PublicSafety,
        ParksMaintenance,
        NoiseComplaint,
        Other
    }

    public static class CategoryExtensions
    {
        public static string GetDescription(this IssueCategory category)
        {
            switch (category)
            {
                case IssueCategory.RoadMaintenance:
                    return "Road Maintenance (Potholes, Repairs)";
                case IssueCategory.StreetLighting:
                    return "Street Lighting Issues";
                case IssueCategory.WaterSupply:
                    return "Water Supply Problems";
                case IssueCategory.Sewage:
                    return "Sewage and Drainage";
                case IssueCategory.GarbageCollection:
                    return "Garbage Collection";
                case IssueCategory.PublicSafety:
                    return "Public Safety Concerns";
                case IssueCategory.ParksMaintenance:
                    return "Parks and Recreation";
                case IssueCategory.NoiseComplaint:
                    return "Noise Complaints";
                default:
                    return "Other";
            }
        }
    }
}
