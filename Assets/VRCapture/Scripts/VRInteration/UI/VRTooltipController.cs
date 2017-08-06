using UnityEngine;
using System.Collections;
namespace VRCapture {
    /// <summary>
    /// Manage all tooltips.
    /// </summary>
    public class VRTooltipController : MonoBehaviour {

        public enum TooltipButtons {
            TriggerTooltip,
            GripTooltip,
            TouchpadTooltip,
            AppMenuTooltip,
            None
        }

        [Tooltip("The text to display for the trigger button action.")]
        public string triggerText;
        [Tooltip("The text to display for the grip button action.")]
        public string gripText;
        [Tooltip("The text to display for the touchpad action.")]
        public string touchpadText;
        [Tooltip("The text to display for the application menu button action.")]
        public string appMenuText;
        [Tooltip("The colour to use for the tooltip background container.")]
        public Color tipBackgroundColor = Color.black;
        [Tooltip("The colour to use for the text within the tooltip.")]
        public Color tipTextColor = Color.white;
        [Tooltip("The colour to use for the line between the tooltip and the relevant controller button.")]
        public Color tipLineColor = Color.black;

        private bool triggerInit = false;
        private bool gripInit = false;
        private bool touchpadInit = false;
        private bool appMenuInit = false;


        private void Awake() {
            triggerInit = false;
            gripInit = false;
            touchpadInit = false;
            appMenuInit = false;
            InitTips();
        }
        /// <summary>
        /// Init all tooltips
        /// </summary>
        private void InitTips() {
            foreach (var tooltip in GetComponentsInChildren<VRTooltip>()) {
                var tipText = "";
                Transform tipTransform = null;

                switch (tooltip.name.Replace("Tooltip", "").ToLower()) {
                    case "trigger":
                        tipText = triggerText;
                        tipTransform = GetTransform("trigger");
                        if (tipTransform != null) {
                            triggerInit = true;
                        }
                        break;
                    case "grip":
                        tipText = gripText;
                        tipTransform = GetTransform("lgrip"); ;
                        if (tipTransform != null) {
                            gripInit = true;
                        }
                        break;
                    case "touchpad":
                        tipText = touchpadText;
                        tipTransform = GetTransform("trackpad"); ;
                        if (tipTransform != null) {
                            touchpadInit = true;
                        }
                        break;
                    case "appmenu":
                        tipText = appMenuText;
                        tipTransform = GetTransform("button"); ;
                        if (tipTransform != null) {
                            appMenuInit = true;
                        }
                        break;
                }

                tooltip.displayText = tipText;
                tooltip.drawLineTo = tipTransform;

                tooltip.containerColor = tipBackgroundColor;
                tooltip.fontColor = tipTextColor;
                tooltip.lineColor = tipLineColor;

                tooltip.Reset();

                if (tipText.Trim().Length == 0) {
                    tooltip.gameObject.SetActive(false);
                }
            }
        }
        /// <summary>
        /// searching corresponding vive trackobject
        /// </summary>
        /// <param name="findTransform"></param>
        /// <returns></returns>
        private Transform GetTransform(string findTransform) {
            return transform.parent.Find("Model/" + findTransform + "/attach");
        }

        private void Update() {
            if (!(triggerInit && gripInit && touchpadInit && appMenuInit)) {
                InitTips();
            }
        }
    }
}

