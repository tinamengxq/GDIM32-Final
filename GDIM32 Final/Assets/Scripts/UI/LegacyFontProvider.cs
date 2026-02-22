using System;
using UnityEngine;

public static class LegacyFontProvider
{
    private static Font cachedFont;

    public static Font GetFont()
    {
        if (cachedFont != null)
        {
            return cachedFont;
        }

        try
        {
            cachedFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }
        catch (ArgumentException)
        {
            cachedFont = null;
        }

        if (cachedFont == null)
        {
            try
            {
                // Fallback for older Unity versions.
                cachedFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }
            catch (ArgumentException)
            {
                cachedFont = null;
            }
        }

        if (cachedFont == null)
        {
            cachedFont = Font.CreateDynamicFontFromOSFont("Arial", 16);
        }

        return cachedFont;
    }
}
