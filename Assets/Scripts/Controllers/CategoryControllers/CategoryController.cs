using UnityEngine;
using UnityEngine.UI;

public abstract class CategoryController : MonoBehaviour, ICategoryController
{
    protected readonly Color defaultColor = new Color(0.494f, 0.494f, 0.494f);
    protected readonly Color pointsPickedColor = Color.black;

    public Text points;

    public abstract int calculatePoints(Round round);
    public virtual void updateCategory(Round round, bool isAlreadyWon)
    {
        points.text = calculatePoints(round).ToString();
        if (isAlreadyWon)
        {
            points.color = pointsPickedColor;
        }
    }

    public virtual void resetUI()
    {
        points.text = "0";
        points.color = defaultColor;
    }
}
