using UnityEngine;

public class Viewport : Singleton<Viewport>
{
    float minX;
    float maxX;
    float minY;
    float maxY;
    float middleX;
    float middleY;
    float diagonal;
    int screenWidth;
    int screenHeight;
    public float MaxX => maxX;

    void Start()
    {
        Camera mainCamera = Camera.main;

        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));

        middleX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).x;
        middleY = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).y;


        minX = bottomLeft.x;
        minY = bottomLeft.y;
        maxX = topRight.x;
        maxY = topRight.y;

        screenWidth = (int)(maxX - minX);
        screenHeight = (int)(maxY - minY);
        diagonal = Mathf.Sqrt(screenWidth * screenWidth + screenHeight * screenHeight);

    }

    public Vector3 PlayerMoveablePosition(Vector3 playerPosition, float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Clamp(playerPosition.x, minX + paddingX, maxX - paddingX);
        position.y = Mathf.Clamp(playerPosition.y, minY + paddingY, maxY - paddingY);

        return position;
    }

    public Vector3 RandomEnemySpawnPosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.x = maxX + paddingX;
        position.y = Random.Range(minY + paddingY, maxY - paddingY);

        return position;
    }

    public Vector3 RandomRightHalfPosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.x = Random.Range(middleX, maxX - paddingX);
        position.y = Random.Range(minY + paddingY, maxY - paddingY);

        return position;
    }

    public Vector3 RandomEnemyMovePosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.x = Random.Range(minX + paddingX, maxX - paddingX);
        position.y = Random.Range(minY + paddingY, maxY - paddingY);

        return position;
    }

    public Vector3 RandomEnemyBirthPosition(float paddingX, float paddingY)
    {
        // 生成一个随机方向的单位向量
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        // 计算目标位置,距离屏幕中心半个对角线长度
        Vector3 position = new Vector3(middleX + randomDir.x * diagonal / 2, middleY + randomDir.y * diagonal / 2, 0);

        // 确认位置在屏幕外
        // if (Mathf.Abs(position.x) < screenWidth / 2 && Mathf.Abs(position.y) < screenHeight / 2)
        // {
        //     position = -position;
        // }
        return position;
    }
}