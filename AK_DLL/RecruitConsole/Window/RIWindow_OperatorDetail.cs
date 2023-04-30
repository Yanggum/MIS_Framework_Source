﻿using System;
using Verse;
using RimWorld.Planet;
using System.Text;
using RimWorld;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using FSUI;

namespace AK_DLL
{
    #region legacy 
    /*
    public class RIWindow_OperatorDetail : Dialog_NodeTree
    {
        public static bool isRecruit = true;
        public static readonly Color StackElementBackground = new Color(1f, 1f, 1f, 0.1f);
        public RIWindow_OperatorDetail(DiaNode startNode, bool radioMode) : base(startNode, radioMode, false, null)
        {
        }
        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(1920f, 1080f);
            }
        }

        public Texture2D blackBack
        {
            get
            {
                return ContentFinder<Texture2D>.Get("UI/Frame/Frame_Skills");
            }
        }
        public OperatorDef Operator_Def
        {
            get { return RIWindowHandler.def; }
        }
        public Thing RecruitConsole
        {
            get { return RIWindowHandler.recruitConsole; }
        }
        
        public override void DoWindowContents(Rect inRect)
        {
            Color temp = GUI.color;       
            try
            {
                Widgets.DrawTextureFitted(new Rect(inRect.x += 350f + Operator_Def.standOffset.x, inRect.y + 40f + Operator_Def.standOffset.y, inRect.width - 870f, inRect.height), ContentFinder<Texture2D>.Get(Operator_Def.stand), Operator_Def.standRatio);
            }
            catch
            {
                Log.Error("MIS. 立绘错误");
            }
            //立绘绘制
            GUI.DrawTexture(new Rect(970f, 0f, 550f, 720f), blackBack);
            //背景绘制
            Rect rect = new Rect(1000f, 20f, 150f, 70f);

            //返回按钮的绘制
            Rect rect_Back = new Rect(1130f, 620f, 100f, 60f);
            if (Widgets.ButtonText(rect_Back, "AK_Back".Translate()) || KeyBindingDefOf.Cancel.KeyDownEvent)
            {
                this.Close();
                RIWindowHandler.OpenRIWindow(RIWindowType.Op_List);
            }

            Widgets.Label(rect, Operator_Def.nickname + "：" + Operator_Def.name);
            //人名绘制
            Rect rect1 = new Rect(rect);
            rect.y += 20f;
            rect.height += 35f;
            Widgets.Label(rect, Operator_Def.description);
            //描述绘制
            rect.height -= 35f;
            rect.y += 100f;
            Widgets.Label(rect, "AK_Terait".Translate());
            rect.y += 25f;
            foreach (TraitAndDegree TraitAndDegree in Operator_Def.traits)
            {
                TraitDegreeData traitDef = TraitAndDegree.def.DataAtDegree(TraitAndDegree.degree);
                if (traitDef == null)
                {
                    Log.ErrorOnce($"MIS. {this.Operator_Def}'s {TraitAndDegree.def.defName} do not have {TraitAndDegree.degree} degree", 1);
                }
                else
                {
                    Rect traitRect = new Rect(rect.x, rect.y, Text.CalcSize(traitDef.label).x + 10f, 25);
                    temp = GUI.color;
                    GUI.color = StackElementBackground;
                    GUI.DrawTexture(traitRect, BaseContent.WhiteTex);
                    GUI.color = temp;
                    Text.Anchor = TextAnchor.MiddleCenter;
                    Widgets.Label(traitRect, traitDef.label.Truncate(traitRect.width));
                    Text.Anchor = TextAnchor.UpperLeft;
                    if (Mouse.IsOver(traitRect)) { Widgets.DrawHighlight(traitRect); }
                    rect.y += 28f;
                }
                /*string label = "寄";
                label = TraitAndDegree.def.DataAtDegree(TraitAndDegree.degree)?.label;
                Widgets.Label(rect, label ?? "寄");*//*
            }
            //特性显示绘制
            Rect rect_AbilityImage = new Rect(rect.x, rect.y + 65f, 60f, 60f);
            Rect rect_AbilityText = new Rect(rect.x + 70f, rect.y + 50f, 100f, 60f);

            if (Operator_Def.abilities != null && Operator_Def.abilities.Count > 0)
            {
                foreach (OperatorAbilityDef ability in Operator_Def.abilities)
                {
                    Texture2D abilityImage = ContentFinder<Texture2D>.Get(ability.icon);
                    Widgets.DrawTextureFitted(rect_AbilityImage, abilityImage, 1f);
                    StringBuilder text = new StringBuilder();
                    text.AppendLine(ability.label);
                    text.AppendLine(ability.description);
                    Widgets.Label(rect_AbilityText, text.ToString().Trim());
                    rect_AbilityImage.y += 65f;
                    rect_AbilityText.y += 65f;
                }
            }
            //^绘制技能(放的那种)

            rect1.x = 80f;
            rect1.y = 350f;
            rect1.width -= 60f;
            rect1.height -= 30f;
            Texture2D smallFire = ContentFinder<Texture2D>.Get("UI/Icons/PassionMinor");
            Texture2D bigFire = ContentFinder<Texture2D>.Get("UI/Icons/PassionMajor");
            //获取兴趣贴图

            Rect rect2 = new Rect(rect1);
            rect2.width += 100f;
            rect2.x += 70f;
            Rect rect3 = new Rect(rect1);
            rect3.height = 152f;
            rect3.width = 152f;
            rect3.y -= 250f;
            Widgets.DrawTextureFitted(new Rect(rect3.x + Operator_Def.headPortraitOffset.x, rect3.y + Operator_Def.headPortraitOffset.y, rect3.width, rect3.height), ContentFinder<Texture2D>.Get("UI/Frame/Frame_HeadPortrait"), 1f);
            Widgets.DrawTextureFitted(new Rect(rect3.x + 3f + Operator_Def.headPortraitOffset.x, rect3.y + 2f + Operator_Def.headPortraitOffset.y, 145f, 148f), ContentFinder<Texture2D>.Get(Operator_Def.headPortrait), 1f);
            //绘制头像框与头像
            rect3.height = 150f;
            rect3.width = 150f;
            rect3.x += 5f;
            Widgets.DrawTextureFitted(new Rect(rect2.x - 45f, rect2.y + 95f, 180f, 105f), blackBack, 3f);

            foreach (SkillAndFire skillAndFire in Operator_Def.Skills)
            {
                int skillLv;
                if (GameComp_OperatorDocumentation.operatorDocument.ContainsKey(Operator_Def.OperatorID))
                {
                    skillLv = GameComp_OperatorDocumentation.operatorDocument[Operator_Def.OperatorID].skillLevel[skillAndFire.skill];
                }
                else
                {
                    skillLv = skillAndFire.level;
                }
                float verticalOffset = 25f * TypeDef.statType[skillAndFire.skill.defName];
                Widgets.FillableBar(new Rect(rect2.x, rect2.y + verticalOffset, 170f, 20f), skillLv / 20f, SolidColorMaterials.NewSolidColorTexture(new Color(1f, 1f, 1f, 0.3f)), ContentFinder<Texture2D>.Get("UI/Frame/Null"), false);
                Widgets.Label(new Rect(rect1.x - 35f, rect1.y + verticalOffset, 150f, rect1.height), skillAndFire.skill.label);
                Widgets.Label(new Rect(rect1.x + 50f, rect1.y + verticalOffset, 100f, rect1.height), skillLv.ToString());
                Rect rect4 = new Rect(rect1.x + 25f, rect1.y + 4f + verticalOffset, 10f, 10f);
                if (skillAndFire.fireLevel == Passion.Minor)
                {
                    Widgets.DrawTextureFitted(rect4, smallFire, 2.5f);
                }
                if (skillAndFire.fireLevel == Passion.Major)
                {
                    Widgets.DrawTextureFitted(rect4, bigFire, 2.5f);
                }
            }
            //技能绘制


            rect_Back.x -= 145f;
            OperatorDocument doc = null;
            if (GameComp_OperatorDocumentation.operatorDocument.ContainsKey(Operator_Def.OperatorID))
            {
                doc = GameComp_OperatorDocumentation.operatorDocument[Operator_Def.OperatorID];
            }

            if (Widgets.ButtonText(rect_Back, recruitText))
            {
                if (isRecruit == false)
                {
                    isRecruit = true;
                    AK_ModSettings.secretary = AK_Tool.GetOperatorIDFrom(Operator_Def.defName);
                    this.Close();
                    RIWindowHandler.OpenRIWindow(RIWindowType.MainMenu);
                    return;
                }

                //如果招募曾经招过的干员
                if (doc != null && !doc.currentExist)
                {
                }
                //如果干员未招募过，或已死亡
                if (RecruitConsole.TryGetComp<CompRefuelable>().Fuel >= Operator_Def.ticketCost - 0.01)
                {
                    if (doc == null || !doc.currentExist)
                    {
                        RecruitConsole.TryGetComp<CompRefuelable>().ConsumeFuel(Operator_Def.ticketCost);
                        Operator_Def.Recruit(RecruitConsole.Map);
                        this.Close();

                        /*RIWindow_OperatorList window = new RIWindow_OperatorList(new DiaNode(new TaggedString()), true);
                        window.soundAmbient = SoundDefOf.RadioComms_Ambience;
                        Find.WindowStack.Add(window);*//*
                    }
                    else
                    {
                        recruitText = "AK_CanntRecruitOperator".Translate();
                    }
                }
                else
                {
                    recruitText = "AK_NoTicket".Translate();
                }
                
            }
            //招募
            //切换技能
            if (false && doc != null && doc.currentExist)
            {
                rect_Back.x -= 145f;
                if (Widgets.ButtonText(rect_Back, switchSkillText))
                {
                    doc.groupedAbilities[doc.preferedAbility].enabled = false;
                    doc.preferedAbility = (doc.preferedAbility + 1) % doc.groupedAbilities.Count;
                    doc.groupedAbilities[doc.preferedAbility].enabled = true;
                    Log.Message($"切换技能至{doc.groupedAbilities[doc.preferedAbility].AbilityDef.defName}");
                }
            }  
        }

        private string switchSkillText = "AK_SwitchSkill".Translate();
        public string recruitText = "AK_RecruitOperator".Translate();
    }*/
    #endregion

    public class RIWindow_OperatorDetail : RIWindow
    {
        public static bool isRecruit = true;

        OperatorDocument doc = null;

        static string recruitText;

        private bool canRecruit;

        int preferredSkin = 1;

        static int preferredVanillaSkillChart = 0;

        Dictionary<int, GameObject> fashionBtns;

        List<GameObject> vanillaSkillBtns;  //0,1: 条形图; 2,3: 雷达图

        List<GameObject> opSkills;  //只有可选技能被加进来。

        private OperatorDef Def
        {
            get { return RIWindowHandler.def; }
        }
        public Thing RecruitConsole
        {
            get { return RIWindowHandler.recruitConsole; }
        }
        private GameObject ClickedBtn
        {
            get
            {
                return EventSystem.current.currentSelectedGameObject;
            }
        }

        private GameObject ClickedBtnParent
        {
            get
            {
                return ClickedBtn.transform.parent.gameObject;
            }
        }

        private int btnOrder(GameObject clickedBtn)
        {
            return int.Parse(clickedBtn.name.Substring(RIWindow_OperatorList.orderInName));
        }

        private int PreferredAbility
        {
            get { return doc.preferedAbility; }
            set { doc.preferedAbility = value; }
        }

        public override void DoContent()
        {
            DrawNavBtn();
            Initialization();
            DrawFashionBtn();
            ChangeStandTo(preferredSkin, true);

            DrawOperatorAbility();
            if (doc != null) SwitchGroupedSkillTo(doc.preferedAbility);

            DrawWeapon();
            DrawTrait();

            DrawVanillaSkills();
            ChangeVanillaSkillChartTo(preferredVanillaSkillChart);

            DrawDescription();
        }

        private void Initialization()
        {
            if (GameComp_OperatorDocumentation.operatorDocument.ContainsKey(Def.OperatorID))
            {
                doc = GameComp_OperatorDocumentation.operatorDocument[Def.OperatorID];
                preferredSkin = doc.preferedSkin;
            }
            canRecruit = false;
            if (RecruitConsole.TryGetComp<CompRefuelable>().Fuel >= Def.ticketCost - 0.01)
            {
                if (doc == null || !doc.currentExist)
                {
                    canRecruit = true;
                    recruitText = "可以招募"; //残留
                }
                else
                {
                    recruitText = "AK_CanntRecruitOperator".Translate();
                }
            }
            else
            {
                recruitText = "AK_NoTicket".Translate();
            }
        }

        //确认招募和取消也是导航键
        private void DrawNavBtn()
        {
            GameObject navBtn;
            //Home
            navBtn = GameObject.Find("BtnHome");
            navBtn.GetComponentInChildren<Button>().onClick.AddListener(delegate()
            {
                RIWindowHandler.OpenRIWindow(RIWindowType.MainMenu);
                this.Close();
            });
            //取消
            navBtn = GameObject.Find("BtnCancel");
            navBtn.GetComponentInChildren<Button>().onClick.AddListener(delegate()
            {
                RIWindowHandler.OpenRIWindow(RIWindowType.Op_List);
                this.Close(false);
            });
            //确认招募/更换助理
            navBtn = GameObject.Find("BtnConfirm");
            Button button = navBtn.GetComponentInChildren<Button>();
            if (isRecruit)
            {
                //FIXME: 更换贴图
                button.onClick.AddListener(delegate ()
                {
                    //如果招募曾经招过的干员
                    if (doc != null && !doc.currentExist)
                    {
                    }
                    //如果干员未招募过，或已死亡
                    if (canRecruit)
                    {
                        RecruitConsole.TryGetComp<CompRefuelable>().ConsumeFuel(Def.ticketCost);
                        Def.Recruit(RecruitConsole.Map);
                        this.Close(true);
                        if (false)
                        {
                            RIWindowHandler.OpenRIWindow(RIWindowType.Op_List);
                        }
                        /*RIWindow_OperatorList window = new RIWindow_OperatorList(new DiaNode(new TaggedString()), true);
                        window.soundAmbient = SoundDefOf.RadioComms_Ambience;
                        Find.WindowStack.Add(window);*/
                    }
                });
            }
            else
            {
                button.GetComponent<Image>().sprite = AK_Tool.FSAsset.LoadAsset<Sprite>("ChangeSec");
                //fixme
                button.onClick.AddListener(delegate ()
                {
                    isRecruit = true;
                    AK_ModSettings.secretary = AK_Tool.GetOperatorIDFrom(Def.defName);
                    RIWindowHandler.OpenRIWindow(RIWindowType.MainMenu);
                    this.Close();
                });
            }
        }

        //换装按钮会被记录于 this.fashionbtns
        private void DrawFashionBtn()
        {
            Transform FashionPanel = GameObject.Find("FashionPanel").transform;
            GameObject fashionIconPrefab = AK_Tool.FSAsset.LoadAsset<GameObject>("FashionIcon");
            GameObject fashionIcon;
            Vector3 v3;

            fashionBtns = new Dictionary<int, GameObject>();

            fashionIcon = GameObject.Find("Elite0");
            fashionBtns.Add(0, fashionIcon);
            if (Def.commonStand != null)
            {
                fashionIcon.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
                {
                    ChangeStandTo(0);
                });
            }
            else fashionIcon.SetActive(false);

            fashionIcon = GameObject.Find("Elite2");
            fashionBtns.Add(1, fashionIcon);
            fashionIcon.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
            {
                ChangeStandTo(1);
            });

            if (Def.fashion != null)
            {
                v3 = fashionIconPrefab.transform.localPosition;
                for (int i = 0; i < Def.fashion.Count; ++i)
                {
                    //逻辑顺序 代表这按钮在面板上实际的位置（即精2按钮之后）
                    int logicOrder = i + 2;
                    fashionIcon = GameObject.Instantiate(fashionIconPrefab);
                    fashionIcon.transform.localPosition = new Vector3(v3.x * logicOrder, v3.y);
                    fashionIcon.SetActive(true);
                    fashionIcon.name = "FSUI_FashIc_" + logicOrder;
                    fashionIcon.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
                    {
                        ChangeStandTo(btnOrder(ClickedBtn));
                    });
                    fashionBtns.Add(logicOrder, fashionIcon);
                }
            }
        }

        //可能要做2种
        private void DrawVanillaSkills()
        {
            //柱状图按钮
            GameObject skillTypeBtn = GameObject.Find("BtnBarChart");
            vanillaSkillBtns = new List<GameObject>();
            vanillaSkillBtns.Add(skillTypeBtn);
            skillTypeBtn.GetComponentInChildren<Button>().onClick.AddListener(delegate()
            {
                preferredVanillaSkillChart = 0;
                ChangeVanillaSkillChartTo(0);
            });

            //柱状图
            //GameObject skillBarPanelPrefab = AK_Tool.FSAsset.LoadAsset<GameObject>("SkillBarPanel");
            GameObject skillBarPanel = GameObject.Find("SkillBarPanel");
            GameObject skillBar;
            for (int i = 0; i < TypeDef.SortOrderSkill.Count; ++i)
            {
                skillBar = skillBarPanel.transform.GetChild(i).gameObject;
                //技能名字
                skillBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Def.SortedSkills[i].skill.label.Translate();
                //技能等级 显示与滑动条
                skillBar.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text =  Def.SortedSkills[i].level.ToString();
                skillBar.GetComponentInChildren<Slider>().value = Def.SortedSkills[i].level;
            }
            vanillaSkillBtns.Add(skillBarPanel);

            //雷达图按钮
            skillTypeBtn = GameObject.Find("BtnRadarChart");
            vanillaSkillBtns.Add(skillTypeBtn);

            skillTypeBtn.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
            {
                preferredVanillaSkillChart = 2;
                ChangeVanillaSkillChartTo(2);
            });

            //
            GameObject radarPanel = GameObject.Find("SkillRadarPanel");
            vanillaSkillBtns.Add(radarPanel);
            RadarChart radarChart = radarPanel.GetComponentInChildren<RadarChart>();
            radarChart.data.Add(new GraphData());
            radarChart.data[0].values = new List<float>();
            radarChart.data[0].color = new Color(0.9686275f, 0.5882353f, 0.03137255f);
            for (int i = 0; i < TypeDef.SortOrderSkill.Count; ++i)
            {
                radarChart.vertexLabelValues[i] = Def.SortedSkills[i].skill.label.Translate() + Def.SortedSkills[i].level.ToString();
                radarChart.data[0].values.Add((float)Def.SortedSkills[i].level / 20.0f);
            }
        }

        //0和2是按钮，1和3是图表本身
        private void ChangeVanillaSkillChartTo(int val)
        {
            if (val == 0)
            {
                vanillaSkillBtns[0].transform.GetChild(0).gameObject.SetActive(true);
                vanillaSkillBtns[1].SetActive(true);
                vanillaSkillBtns[2].transform.GetChild(0).gameObject.SetActive(false);
                vanillaSkillBtns[3].SetActive(false);
            }
            else
            {
                vanillaSkillBtns[0].transform.GetChild(0).gameObject.SetActive(false);
                vanillaSkillBtns[1].SetActive(false);
                vanillaSkillBtns[2].transform.GetChild(0).gameObject.SetActive(true);
                vanillaSkillBtns[3].SetActive(true);
            }
        }

        private void DrawDescription()
        {
            GameObject OpDescPanel = GameObject.Find("OpDescPanel");
            OpDescPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Def.nickname.Translate();
            OpDescPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Def.description.Translate();

        }

        private void DrawWeapon()
        {
            GameObject WeaponPanel = GameObject.Find("Weapon");
        }

        private void DrawOperatorAbility()
        {
            int skillCnt = Def.abilities.Count;
            if (skillCnt == 0) return;
            Transform opAbilityPanel = GameObject.Find("OpAbilityPanel").transform;
            GameObject opAbilityPrefab = AK_Tool.FSAsset.LoadAsset<GameObject>("OpAbilityIcon");
            GameObject opAbilityInstance;
            opSkills = new List<GameObject>();
            int logicOrder = 0; //在技能组内，实际的顺序
            for (int i = 0; i < skillCnt; ++i)
            {
                OperatorAbilityDef opAbilty = Def.abilities[i];
                opAbilityInstance = GameObject.Instantiate(opAbilityPrefab, opAbilityPanel);
                Texture2D icon = opAbilty.Icon;
                opAbilityInstance.GetComponent<Image>().sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector3.zero);
                opAbilityInstance.name = "FSUI_OpAbil_" + logicOrder;
                logicOrder++;
                opAbilityInstance.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
                {
                    SwitchGroupedSkillTo(btnOrder(ClickedBtn));
                });
                
                //右下角的勾 常驻技能橙色。
                if (!opAbilty.grouped)
                {
                    opAbilityInstance.transform.GetChild(1).GetComponent<Image>().sprite = AK_Tool.FSAsset.LoadAsset<Sprite>("InnateAb");
                }
                else
                {
                    opSkills.Add(opAbilityInstance);
                    opAbilityInstance.transform.GetChild(1).gameObject.SetActive(false);
                }

                opAbilityInstance.SetActive(true);
            }
        }

        private void SwitchGroupedSkillTo(int val)
        {
            Log.Message($"try s skills to {val}");
            if (doc == null || doc.currentExist == false) return;
            opSkills[PreferredAbility].transform.GetChild(1).gameObject.SetActive(false);
            PreferredAbility = val;
            opSkills[PreferredAbility].transform.GetChild(1).gameObject.SetActive(true);
        }

        private void DrawStand()
        {
            Image opStand = GameObject.Find("OpStand").GetComponent<Image>();
            Texture2D tex;
            if (preferredSkin == 0) tex = ContentFinder<Texture2D>.Get(Def.commonStand);
            else if (preferredSkin == 1) tex = ContentFinder<Texture2D>.Get(Def.stand);
            else tex = ContentFinder<Texture2D>.Get(Def.fashion[preferredSkin - 2]);
            opStand.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }

        private void DrawTrait()
        {
            if (Def.traits == null || Def.traits.Count == 0) return;
            GameObject traitPrefab = AK_Tool.FSAsset.LoadAsset<GameObject>("TraitTemplate");
            GameObject traitInstance;
            Transform traitPanel = GameObject.Find("ActualTraitsPanel").transform;

            for (int i = 0; i < Def.traits.Count; ++i)
            {
                traitInstance = GameObject.Instantiate(traitPrefab, traitPanel); 
                TraitDegreeData traitDef = Def.traits[i].def.DataAtDegree(Def.traits[i].degree);
                traitInstance.GetComponentInChildren<TextMeshProUGUI>().text = traitDef.label.Translate();
            }
        }

        private void ChangeStandTo(int val, bool forceChange = false)
        {
            GameObject fBtn;
            if (!forceChange && val == preferredSkin) return;

            if (doc != null)
            {
                doc.preferedSkin = val;
            }
            //禁用之前的换装按钮
            fBtn = fashionBtns[preferredSkin];
            fBtn.transform.GetChild(0).gameObject.SetActive(true);
            fBtn.transform.GetChild(1).gameObject.SetActive(false);

            preferredSkin = val;
            fBtn = fashionBtns[preferredSkin];
            fBtn.transform.GetChild(0).gameObject.SetActive(false);
            fBtn.transform.GetChild(1).gameObject.SetActive(true);
            DrawStand();
        }
    }
}