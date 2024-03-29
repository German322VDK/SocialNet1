﻿using System.Collections.Generic;

namespace SocialNet1.Data
{
    public static class CooridnatesNames
    {
        private static Dictionary<string, string> _coordinates = new Dictionary<string, string> 
        {
            {"0d0", "нормис" },

            {"1d1", "право-либ" },
            {"1d2", "верующий" },
            {"2d1", "патриот" },
            {"2d2", "консерватор" },

            {"1d-1", "бумер" },
            {"1d-2", "думер" },
            {"2d-1", "клас. либ" },
            {"2d-2", "зумер" },

            {"-1d1", "бюрократ" },
            {"-1d2", "этатист" },
            {"-2d1", "соц-дем" },
            {"-2d2", "социалист" },

            {"-1d-1", "аполитичный" },
            {"-1d-2", "хиппи" },
            {"-2d-1", "лево-либ" },
            {"-2d-2", "прогрессивный" },
        };
        public static string GetName(int x, int y)
        {
            var name = $"{x}d{y}";

            return _coordinates[name];
        }
    }
}
