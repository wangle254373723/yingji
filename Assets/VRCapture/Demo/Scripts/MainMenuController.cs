using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using UnityEngine.SceneManagement;

namespace VRCapture.Demo {
    public class MainMenuController : MonoBehaviour {
        public CameraControllerType cameraControllerType = CameraControllerType.none;
        public GameObject Camera;
        public Transform target = null;
        public VREventController rightEventController;
        public VREventController leftEventController;
        public string demoBeginIntroduceText;
        public string demoOneIntroduceText;
        public string demoTwoIntroduceText;
        public string demoThreeIntroduceText;
        private GameObject mainMenu;
        private GameObject demoUI;
        private GameObject sceneOne;
        private GameObject sceneTwo;
        private GameObject showVideoObject;
        private Button backButton;
        private Button exitButton;
        private Button videoFilePath;
        private Button cameraScence1Button;
        private Button cameraScence2Button;
        private Button showVideoBackButton;
        private Text demoIntroduceText;
        private VRRadialMenu radiaMenu;
        private UIControllerManager uiControllerManager;
        private VRTeleport teleport;
        private Text videoFileText;
        private TooltipsManager rightTooltipController;
        private TooltipsManager leftTooltipController;
        // Use this for initialization
        void Start() {
            mainMenu = this.transform.Find("MainMenu").gameObject;
            demoUI = this.transform.Find("DemoSceneUI").gameObject;
            sceneOne = this.transform.Find("SceneOneObject").gameObject;
            sceneTwo = this.transform.Find("SceneTwoObject").gameObject;
            exitButton = mainMenu.transform.Find("Exit").GetComponent<Button>();
            videoFilePath = mainMenu.transform.Find("VideoFilePath").GetComponent<Button>();
            cameraScence1Button = mainMenu.transform.Find("RayCamera").GetComponent<Button>();
            cameraScence2Button = mainMenu.transform.Find("TouchCamera").GetComponent<Button>();
            backButton = demoUI.transform.Find("BackButton").GetComponent<Button>();
            demoIntroduceText = demoUI.transform.Find("Text").GetComponent<Text>();
            showVideoBackButton = demoUI.transform.Find("ShowVideoBackButton").GetComponent<Button>();
            showVideoObject = this.transform.Find("ShowVideoObject").gameObject;
            videoFileText = showVideoObject.transform.Find("VideoFileText").GetComponent<Text>();
            Init();
        }

        void Init() {
            AddButtonEvent(backButton);
            AddButtonEvent(exitButton);
            AddButtonEvent(videoFilePath);
            AddButtonEvent(cameraScence1Button);
            AddButtonEvent(cameraScence2Button);
            AddButtonEvent(showVideoBackButton);
            if (sceneOne != null) {
                sceneOne.SetActive(false);
            }
            if (sceneTwo != null) {
                sceneTwo.SetActive(false);
            }
            if (demoUI != null) {
                InitDemoUI();
            }
            if (rightEventController != null) {
                radiaMenu = rightEventController.transform.GetComponentInChildren<VRRadialMenu>();
                teleport = rightEventController.GetComponent<VRTeleport>();
                uiControllerManager = rightEventController.GetComponent<UIControllerManager>();
                radiaMenu.gameObject.SetActive(false);
                uiControllerManager.enabled = false;
                teleport.enabled = false;
                if (rightTooltipController == null) {
                    rightTooltipController = rightEventController.GetComponentInChildren<TooltipsManager>();
                }
            }

            if (leftEventController != null) {
                if (leftTooltipController == null) {
                    leftTooltipController = leftEventController.GetComponentInChildren<TooltipsManager>();
                }
            }

            showVideoObject.SetActive(false);
        }
        public void InitDemoUI() {
            showVideoBackButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            demoIntroduceText.text = demoBeginIntroduceText;

        }
        public void Exit() {
            Application.Quit();
        }

        public void VideoFilePath() {
            mainMenu.SetActive(false);
            showVideoObject.SetActive(true);
            if (demoUI.gameObject.activeSelf) {
                for (int i = 0; i < demoUI.transform.childCount; i++) {
                    demoUI.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else {
                demoUI.gameObject.SetActive(true);
            }
            showVideoBackButton.gameObject.SetActive(true);
            backButton.gameObject.SetActive(false);
            demoIntroduceText.text = demoThreeIntroduceText;
            videoFileText.text = "Video File Path:" + VRCapture.Instance.FolderPath.ToString() + "\n" +
                "\n Camera Cache File Path:" + System.IO.Path.GetFullPath(string.Format(@"{0}", "Cache"));
        }

        public void RayCamera() {
            if (rightTooltipController != null) {
                rightTooltipController.isClickTrigger = false;
            }
            if (leftTooltipController != null) {
                leftTooltipController.isClickTrigger = false;
            }
            radiaMenu.gameObject.SetActive(true);
            uiControllerManager.enabled = true;
            uiControllerManager.controllerState = ControllerState.isRay;
            sceneOne.SetActive(true);
            mainMenu.SetActive(false);
            if (demoUI.gameObject.activeSelf) {
                for (int i = 0; i < demoUI.transform.childCount; i++) {
                    demoUI.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else {
                demoUI.gameObject.SetActive(true);
            }
            showVideoBackButton.gameObject.SetActive(false);
            cameraControllerType = CameraControllerType.followCamera;
            Camera = sceneOne.GetComponentInChildren<Camera>().gameObject;
            demoIntroduceText.text = demoOneIntroduceText;
            target = rightEventController.transform.parent;
            VRCapture.Instance.vrCaptureVideos = new VRCaptureVideo[] { sceneOne.GetComponentInChildren<VRCaptureVideo>() };

        }

        public void TouchCamera() {
            if (rightTooltipController != null) {
                rightTooltipController.isClickTrigger = false;
                rightTooltipController.isClickTouchpad = true;
                rightTooltipController.tooltipController.touchpadText = "Click to Move";
            }
            if (leftTooltipController != null) {
                leftTooltipController.tooltipController.triggerText = "Click Choose Button";
            }
            uiControllerManager.enabled = true;
            uiControllerManager.controllerState = ControllerState.isTouch;
            rightEventController.GetComponent<VRUIController>().controllerState = ControllerState.isTouch;
            rightEventController.GetComponent<VRDemoUIPointer>().enabled = false;
            teleport.enabled = true;
            sceneTwo.SetActive(true);
            mainMenu.SetActive(false);
            if (demoUI.gameObject.activeSelf) {
                for (int i = 0; i < demoUI.transform.childCount; i++) {
                    demoUI.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else {
                demoUI.gameObject.SetActive(true);
            }
            showVideoBackButton.gameObject.SetActive(false);
            cameraControllerType = CameraControllerType.PosingCamera;
            Camera = sceneTwo.GetComponentInChildren<Camera>().gameObject;
            demoIntroduceText.text = demoTwoIntroduceText;
            target = rightEventController.transform.parent;
            VRCapture.Instance.vrCaptureVideos = new VRCaptureVideo[] { sceneTwo.GetComponentInChildren<VRCaptureVideo>() };
        }

        void AddButtonEvent(Button button) {
            button.onClick.AddListener(delegate () {
                ButtonClick(button);
            });
        }

        void ButtonClick(Button buttonName) {
            if (buttonName != null) {
                switch (buttonName.name) {
                    case "Exit":
                        Exit();
                        break;
                    case "VideoFilePath":
                        VideoFilePath();
                        break;
                    case "RayCamera":
                        RayCamera();
                        break;
                    case "TouchCamera":
                        TouchCamera();
                        break;
                    case "BackButton":
                        BackEvent();
                        break;
                    case "ShowVideoBackButton":
                        BackEvent();
                        break;
                }
            }
        }

        public void ClickSureButtonEvent() {
            if (cameraControllerType != CameraControllerType.none) {
                if (cameraControllerType == CameraControllerType.followCamera) {
                    this.transform.parent = null;
                    string filepath = System.IO.Path.GetFullPath(string.Format(@"{0}/", "Cache"));
                    if (!Directory.Exists(filepath)) {
                        Directory.CreateDirectory(filepath);
                    }
                    Vector3 distanceVe3 = target.position - this.transform.position;
                    float distanceBetween = Vector3.Distance(target.position, this.transform.position);
                    float hightBetween = this.transform.position.y - target.position.y;
                    string filepaths = filepath + "FollowCameraSpot.xml";
                    if (!File.Exists(filepaths)) {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlElement root = xmlDoc.CreateElement("transforms");
                        XmlElement elmNew = xmlDoc.CreateElement("position");
                        XmlElement rotationX = xmlDoc.CreateElement("x");
                        rotationX.InnerText = distanceVe3.x.ToString();
                        XmlElement rotationY = xmlDoc.CreateElement("y");
                        rotationY.InnerText = distanceVe3.y.ToString();
                        XmlElement rotationZ = xmlDoc.CreateElement("z");
                        rotationZ.InnerText = distanceVe3.z.ToString();
                        XmlElement distanceV3 = xmlDoc.CreateElement("Distance");
                        distanceV3.InnerText = distanceBetween.ToString();
                        XmlElement distanceY = xmlDoc.CreateElement("DistanceY");
                        distanceY.InnerText = hightBetween.ToString();

                        elmNew.AppendChild(rotationX);
                        elmNew.AppendChild(rotationY);
                        elmNew.AppendChild(rotationZ);
                        root.AppendChild(elmNew);
                        root.AppendChild(distanceV3);
                        xmlDoc.AppendChild(root);
                        xmlDoc.Save(filepaths);
                    }
                    else {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(filepaths);
                        XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
                        foreach (XmlElement xe in nodeList) {
                            if (xe.Name == "position") {
                                foreach (XmlElement x1 in xe.ChildNodes) {
                                    if (x1.Name == "x") {
                                        x1.InnerText = distanceVe3.x.ToString();
                                    }
                                    else if (x1.Name == "y") {
                                        x1.InnerText = distanceVe3.y.ToString();
                                    }
                                    else if (x1.Name == "z") {
                                        x1.InnerText = distanceVe3.z.ToString();
                                    }
                                }
                            }
                            else if (xe.Name == "Distance") {
                                xe.InnerText = distanceBetween.ToString();
                            }
                            else if (xe.Name == "DistanceY") {
                                xe.InnerText = hightBetween.ToString();
                            }
                        }
                        xmlDoc.Save(filepaths);
                    }
                }
                else if (cameraControllerType == CameraControllerType.PosingCamera) {

                    string filepath = System.IO.Path.GetFullPath(string.Format(@"{0}/", "Cache"));
                    if (!Directory.Exists(filepath)) {
                        Directory.CreateDirectory(filepath);
                    }
                    string filepaths = filepath + "PosingCameraSpot.xml";
                    if (!File.Exists(filepaths)) {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlElement root = xmlDoc.CreateElement("transforms");
                        XmlElement elmNew = xmlDoc.CreateElement("position");
                        XmlElement rotation_X = xmlDoc.CreateElement("x");
                        rotation_X.InnerText = Camera.transform.position.x.ToString();
                        XmlElement rotation_Y = xmlDoc.CreateElement("y");
                        rotation_Y.InnerText = Camera.transform.position.y.ToString();
                        XmlElement rotation_Z = xmlDoc.CreateElement("z");
                        rotation_Z.InnerText = Camera.transform.position.z.ToString();
                        elmNew.AppendChild(rotation_X);
                        elmNew.AppendChild(rotation_Y);
                        elmNew.AppendChild(rotation_Z);
                        root.AppendChild(elmNew);
                        xmlDoc.AppendChild(root);
                        xmlDoc.Save(filepaths);
                    }
                    else {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(filepaths);
                        XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
                        foreach (XmlElement xe in nodeList) {
                            if (xe.Name == "position") {
                                foreach (XmlElement x1 in xe.ChildNodes) {
                                    if (x1.Name == "x") {
                                        x1.InnerText = Camera.transform.position.x.ToString();
                                    }
                                    else if (x1.Name == "y") {
                                        x1.InnerText = Camera.transform.position.y.ToString();
                                    }
                                    else if (x1.Name == "z") {
                                        x1.InnerText = Camera.transform.position.z.ToString();
                                    }
                                }
                                break;
                            }
                        }
                        xmlDoc.Save(filepaths);
                    }
                }
                BackEvent();
            }
            else {
                Camera = null;
                target = null;
            }
        }

        private void BackEvent() {
            SceneManager.LoadScene("Demo - VR Capture");
        }
    }
}