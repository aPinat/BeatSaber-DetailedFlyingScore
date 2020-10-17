using System;
using System.Reflection;
using HarmonyLib;
using IPA;
using IPA.Logging;

namespace DetailedFlyingScore
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        private readonly Logger _logger;
        private readonly Harmony _harmony;
        private const string HarmonyId = "de.aPinat.DetailedFlyingScore";

        [Init]
        public Plugin(Logger logger)
        {
            _logger = logger;
            _logger.Debug("Logger initialized.");
            _harmony = new Harmony(HarmonyId);
        }

        [OnEnable]
        public void OnEnable()
        {
            try
            {
                _logger.Debug("Applying Harmony patches.");
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                _logger.Critical("Error applying Harmony patches: " + ex.Message);
                _logger.Debug(ex);
            }
        }

        [OnDisable]
        public void OnDisable()
        {
            try
            {
                _harmony.UnpatchAll(HarmonyId);
            }
            catch (Exception ex)
            {
                _logger.Critical("Error removing Harmony patches: " + ex.Message);
                _logger.Debug(ex);
            }
        }
    }
}