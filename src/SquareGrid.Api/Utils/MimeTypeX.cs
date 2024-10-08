﻿namespace SquareGrid.Api.Utils
{
    public static class MimeTypeX
    {
        public static bool IsValid(string mediaType)
        {
            return MimeTypes.ContainsKey(mediaType);
        }

        public static string GetExtension(string mediaType)
        {
            if (IsValid(mediaType))
            {
                return MimeTypes[mediaType];
            }

            throw new Exception("Mime type is not supported for media");
        }

        private static readonly Dictionary<string, string> MimeTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "image/jpeg", ".jpg" },
            { "image/png", ".png" },
            { "image/gif", ".gif" },
            { "image/bmp", ".bmp" },
            { "image/tiff", ".tiff" },
            { "image/webp", ".webp" },
            { "image/svg+xml", ".svg" }
        };
    }
}
