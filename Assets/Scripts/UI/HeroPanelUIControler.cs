using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor;

//Script that handles RunTime UI logic includes lvup logic
//Name need to be changed!
public class HeroPanelUIControler : MonoBehaviour
{
    public UIDocument heroPanel;
    public UIDocument runTimeUI;
    private VisualElement _heroPanel;
    private bool heroPanelActive;

    void OnEnable()
    {

        _heroPanel = heroPanel.rootVisualElement;
        _heroPanel.Q<Label>("HpPointsLabel").visible = false;
        _heroPanel.Q<Label>("ExpPointsLabel").visible = false;
        _heroPanel.visible = false;
        heroPanelActive = false;

        var runTime = runTimeUI.rootVisualElement;
        runTime.Q<Label>("HpPointsLabel").visible = false;
        runTime.Q<Label>("ExpPointsLabel").visible = false;

        #region events
        EnemyStatsManager.ExpGained += UpdateExpBar;
        EnemyBehaviour.Attacked += UpdateHPBar;
        _heroPanel.Q<Button>("StrLevelUpButton").clicked += IncreaseStrength;
        _heroPanel.Q<Button>("AgiLevelUpButton").clicked += IncreaseAgility;
        _heroPanel.Q<Button>("IntLevelUpButton").clicked += IncreaseIntelect;
        _heroPanel.Q<Button>("StmLevelUpButton").clicked += IncreaseStamina;
        #endregion

        UpdateExpBar();
        UpdateHPBar();
    }

    private void OnDisable()
    {
        #region events
        EnemyStatsManager.ExpGained -= UpdateExpBar;
        EnemyBehaviour.Attacked -= UpdateHPBar;
        if (_heroPanel == null)
            return;
        _heroPanel.Q<Button>("StrLevelUpButton").clicked -= IncreaseStrength;
        _heroPanel.Q<Button>("AgiLevelUpButton").clicked -= IncreaseAgility;
        _heroPanel.Q<Button>("IntLevelUpButton").clicked -= IncreaseIntelect;
        _heroPanel.Q<Button>("StmLevelUpButton").clicked -= IncreaseStamina;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _heroPanel.visible)
        {
            SwitchVisibility();
            UpdatePanel();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            SwitchVisibility();
            UpdatePanel();

            Time.timeScale = 1f;
            if (_heroPanel.visible)
            {
                Time.timeScale = 0f;
            }            
        }
    }

    void SwitchVisibility()
    {
        _heroPanel.visible = !_heroPanel.visible;
        heroPanelActive = !heroPanelActive;
        var runTime = runTimeUI.rootVisualElement;
        runTime.visible = !runTime.visible;
    }

    void IncreaseStrength()
    {
        PlayerDatabase.strength++;
        _heroPanel.Q<Label>("StrLevelLabel").text = PlayerDatabase.strength.ToString();
        UseAttributePoint();
        PlayerDatabase.minDamage += 10;
        PlayerDatabase.maxDamage += 10;
    }

    void IncreaseAgility()
    {
        PlayerDatabase.agility++;
        _heroPanel.Q<Label>("AgiLevelLabel").text = PlayerDatabase.agility.ToString();
        UseAttributePoint();
        PlayerDatabase.movementSpeed += 0.1f;
    }

    void IncreaseIntelect()
    {
        PlayerDatabase.intelect++;
        _heroPanel.Q<Label>("IntLevelLabel").text = PlayerDatabase.intelect.ToString();
        UseAttributePoint();
    }

    void IncreaseStamina()
    {
        PlayerDatabase.stamina++;
        _heroPanel.Q<Label>("StmLevelLabel").text = PlayerDatabase.stamina.ToString();
        UseAttributePoint();
        PlayerDatabase.maxHp += 10;
    }

    void UseAttributePoint()
    {
        PlayerDatabase.attributesPoints--;
        _heroPanel.Q<Label>("AttributesPointsLabel").text = "Remaining Attributes Points:\t" + PlayerDatabase.attributesPoints.ToString();
        if (PlayerDatabase.attributesPoints <= 0)
        {
            DisableAttributesButtons();
        }
    }
    void DisableAttributesButtons()
    {
        _heroPanel.Q<Button>("StrLevelUpButton").visible = false;
        _heroPanel.Q<Button>("AgiLevelUpButton").visible = false;
        _heroPanel.Q<Button>("IntLevelUpButton").visible = false;
        _heroPanel.Q<Button>("StmLevelUpButton").visible = false;
    }

    void EnableAttributesButtons()
    {
        _heroPanel.Q<Button>("StrLevelUpButton").visible = true;
        _heroPanel.Q<Button>("AgiLevelUpButton").visible = true;
        _heroPanel.Q<Button>("IntLevelUpButton").visible = true;
        _heroPanel.Q<Button>("StmLevelUpButton").visible = true;
    }

    void UpdatePanel()
    {
        _heroPanel.Q<Label>("AttributesPointsLabel").text = "Remaining Attributes Points:\t" + PlayerDatabase.attributesPoints.ToString();
        _heroPanel.Q<Label>("StrLevelLabel").text = PlayerDatabase.strength.ToString();
        _heroPanel.Q<Label>("AgiLevelLabel").text = PlayerDatabase.agility.ToString();
        _heroPanel.Q<Label>("IntLevelLabel").text = PlayerDatabase.intelect.ToString();
        _heroPanel.Q<Label>("StmLevelLabel").text = PlayerDatabase.stamina.ToString();
        _heroPanel.Q<Label>("HeroLevelLabel").text = "Level " + PlayerDatabase.lvl.ToString();
        if (PlayerDatabase.attributesPoints > 0 && _heroPanel.visible)
        {
            EnableAttributesButtons();
        }
        else
        {
            DisableAttributesButtons();
        }
    }

    void UpdateExpBar()
    {
        if (PlayerDatabase.currentExp >= PlayerDatabase.expToNextLvl)
        {
            PlayerDatabase.LevelUp();
            UpdatePanel();
        }

        //Update bars inside heroPanel
        _heroPanel.Q<VisualElement>("CurrentExpVE").style.width = 
            new StyleLength(Length.Percent((int)(100 * (PlayerDatabase.currentExp / (float)PlayerDatabase.expToNextLvl))));
        _heroPanel.Q<Label>("ExpPointsLabel").text = 
            PlayerDatabase.currentExp.ToString() + "/" + PlayerDatabase.expToNextLvl.ToString();
        
        //Update bars in a runtime
        runTimeUI.rootVisualElement.Q<VisualElement>("CurrentExpVE").style.width = 
            new StyleLength(Length.Percent((int)(100 * (PlayerDatabase.currentExp / (float)PlayerDatabase.expToNextLvl))));
        runTimeUI.rootVisualElement.Q<Label>("ExpPointsLabel").text = 
            PlayerDatabase.currentExp.ToString() + "/" + PlayerDatabase.expToNextLvl.ToString();
    }

    void UpdateHPBar()
    {
        if (PlayerDatabase.currentHp != PlayerDatabase.maxHp)
        {
            _heroPanel.Q<VisualElement>("CurrentHpVE").ClearClassList();
            _heroPanel.Q<VisualElement>("CurrentHpVE").AddToClassList("notMaxValue");

            runTimeUI.rootVisualElement.Q<VisualElement>("CurrentHpVE").ClearClassList();
            runTimeUI.rootVisualElement.Q<VisualElement>("CurrentHpVE").AddToClassList("notMaxValue");
        }
        else
        {
            _heroPanel.Q<VisualElement>("CurrentHpVE").ClearClassList();
            _heroPanel.Q<VisualElement>("CurrentHpVE").AddToClassList("maxValue");
           
            runTimeUI.rootVisualElement.Q<VisualElement>("CurrentHpVE").ClearClassList();
            runTimeUI.rootVisualElement.Q<VisualElement>("CurrentHpVE").AddToClassList("maxValue");
        }
        
        _heroPanel.Q<VisualElement>("CurrentHpVE").style.width = 
            new StyleLength(Length.Percent((int)(100 * (PlayerDatabase.currentHp / (float)PlayerDatabase.maxHp))));
        _heroPanel.Q<Label>("HpPointsLabel").text = PlayerDatabase.currentHp.ToString() + "/" + PlayerDatabase.maxHp.ToString();

        runTimeUI.rootVisualElement.Q<VisualElement>("CurrentHpVE").style.width = 
            new StyleLength(Length.Percent((int)(100 * (PlayerDatabase.currentHp / (float)PlayerDatabase.maxHp))));
        runTimeUI.rootVisualElement.Q<Label>("HpPointsLabel").text = PlayerDatabase.currentHp.ToString() + "/" + PlayerDatabase.maxHp.ToString();

    }
}
