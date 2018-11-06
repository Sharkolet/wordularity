using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotepadController : MonoBehaviour {
    public Dictionary<RoundWinType, CategoryController> controllers;
    public Text totalWinText;

    public void Start()
    {
        controllers = new Dictionary<RoundWinType, CategoryController>();
        controllers.Add(RoundWinType.Letters3, GameObject.Find(RoundWinType.Letters3.ToString()).GetComponent<LettersCategoryController>());
        controllers.Add(RoundWinType.Letters4, GameObject.Find(RoundWinType.Letters4.ToString()).GetComponent<LettersCategoryController>());
        controllers.Add(RoundWinType.Letters5, GameObject.Find(RoundWinType.Letters5.ToString()).GetComponent<LettersCategoryController>());
        controllers.Add(RoundWinType.Straight, GameObject.Find(RoundWinType.Straight.ToString()).GetComponent<StraightCategoryController>());
        controllers.Add(RoundWinType.Starter, GameObject.Find(RoundWinType.Starter.ToString()).GetComponent<StarterCategoryController>());
        controllers.Add(RoundWinType.Slam, GameObject.Find(RoundWinType.Slam.ToString()).GetComponent<SlamCategoryController>());
        controllers.Add(RoundWinType.Scorer, GameObject.Find(RoundWinType.Scorer.ToString()).GetComponent<ScorerCategoryController>());
        controllers.Add(RoundWinType.Yacht, GameObject.Find(RoundWinType.Yacht.ToString()).GetComponent<YachtCategoryController>());
        controllers.Add(RoundWinType.Bonus, GameObject.Find(RoundWinType.Bonus.ToString()).GetComponent<BonusCategoryController>());
    }

    public int update(Round round, Player player)
    {
        reset();
        int totalWin = 0;
        (controllers[RoundWinType.Bonus] as BonusCategoryController).playerBonusPoints = player.bonusPoints;
        foreach (RoundWinType roundWinType in Enum.GetValues(typeof(RoundWinType)))
        {
            Round playerRound = player.getPlayerRounds().Find(r => r.winType == roundWinType);
            CategoryController controller = controllers[roundWinType];
            controller.updateCategory(playerRound != null ? playerRound : round, 
                playerRound != null || roundWinType == RoundWinType.Bonus);
            if (playerRound != null)
            {
                totalWin += controller.calculatePoints(playerRound);
            }
        }
        totalWin += controllers[RoundWinType.Bonus].calculatePoints(round);

        totalWinText.text = totalWin.ToString();
        return totalWin;
    }

    public int getTotalWin(Player player)
    {
        int totalWin = player.bonusPoints;
        foreach(Round r in player.getPlayerRounds())
        {
            totalWin += controllers[r.winType].calculatePoints(r);
        }

        return totalWin;
    }

    public int getBonusPoints(Round round)
    {
        return controllers[RoundWinType.Bonus].calculatePoints(round);
    }

    public void reset()
    {
        foreach (CategoryController controller in controllers.Values)
        {
            controller.resetUI();
            totalWinText.text = "0";
        }
    }
}
