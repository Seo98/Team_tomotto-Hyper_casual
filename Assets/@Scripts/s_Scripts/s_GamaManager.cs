using UnityEngine;

public class s_GamaManager : MonoBehaviour
{
    public static s_GamaManager s_instance; // �޸𸮿� �Ҵ�
    public s_PlayerInfo s_Player;

    // TODO : ����Ż ���Ѹ� �̵�������� �����ϴٰ� ����
    // ���л��� > �Ը��� ���� Ÿ�ϸ��� �̵�(����ũ����Ʈ ûũ����?) �Ϸ��ߴµ� ��..��...��..

    private void Awake()
    {
        s_instance = this;
    }


}
