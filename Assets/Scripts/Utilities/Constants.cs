using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Constants
    {
        public static readonly IReadOnlyList<Color> Rarity = new List<Color>
        {
            new Color32(160, 160, 160, 255),
            new Color32(255, 255, 255, 255),
            new Color32(70, 130, 255, 255),
            new Color32(255, 170, 40, 255)
        }.AsReadOnly();

        public static readonly IReadOnlyList<Color> NodeColors = new List<Color>
        {
            new Color32(255, 32, 40, 255),
            new Color32(18, 200, 244, 255),
            new Color32(255, 201, 40, 255),
            new Color32(255, 122, 16, 255),
            new Color32(243, 45, 124, 255)
        }.AsReadOnly();
    }
}
