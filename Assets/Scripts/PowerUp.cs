using UnityEngine;

// TODO: 継承を使用してアイテムを作成する。現状は「強化」「弾丸」「スマッシュ」が存在する。
// TODO: 同時にポリモーフィズム/抽象化についても検討する。アイテムによってインジケータの色を変更するなど。
// TODO: クラスのプロパティについてカプセル化を検討する。

public class PowerUp : MonoBehaviour
{
    public POWER_UP_TYPE type;
}
