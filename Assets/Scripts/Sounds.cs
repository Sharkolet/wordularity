using UnityEngine;

public class Sounds : MonoBehaviour
{
    private static Sounds instance = null;

    public static Sounds Instance
    {
        get { return instance; }
    }

    public static AudioSource AMBIENT;
    public static AudioSource BTN_GENERAL;
    public static AudioSource BTN_CORRECT;
    public static AudioSource BTN_INCORRECT;
    public static AudioSource BTN_CLEAR;
    public static AudioSource FINISH_ROUND;
    public static AudioSource DICES_THROW;
    public static AudioSource GORN;
    public static AudioSource FIREWORKS;
    public static AudioSource CROWD;
    public static AudioSource PENCIL;
    public static AudioSource LETTER_PICK;

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        register();
    }

    public void register()
    {
        AMBIENT = Sounds.instance.GetComponents<AudioSource>()[0];
        BTN_GENERAL = Sounds.instance.GetComponents<AudioSource>()[1];
        BTN_CORRECT = Sounds.instance.GetComponents<AudioSource>()[2];
        BTN_INCORRECT = Sounds.instance.GetComponents<AudioSource>()[3];
        BTN_CLEAR = Sounds.instance.GetComponents<AudioSource>()[4];
        FINISH_ROUND = Sounds.instance.GetComponents<AudioSource>()[5];
        DICES_THROW = Sounds.instance.GetComponents<AudioSource>()[6];
        GORN = Sounds.instance.GetComponents<AudioSource>()[7];
        FIREWORKS = Sounds.instance.GetComponents<AudioSource>()[8];
        CROWD = Sounds.instance.GetComponents<AudioSource>()[9];
        PENCIL = Sounds.instance.GetComponents<AudioSource>()[10];
        LETTER_PICK = Sounds.instance.GetComponents<AudioSource>()[11];
    }
}
