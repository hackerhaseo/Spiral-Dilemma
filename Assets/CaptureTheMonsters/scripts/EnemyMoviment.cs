using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour {

    public Rigidbody2D enemyRigibody;
    public SpriteRenderer enemySprite;
    public Color[] colors;
    float time;

    GameManager gm;
    public ParticleSystem particle;

    public int enemyVelocity;

    private void Start()
    {
        enemySprite.color = colors[0];

        gm = FindObjectOfType<GameManager>();

        particle =  Instantiate(particle, Vector3.zero, Quaternion.identity);

        particle.Pause();
    }

    private void FixedUpdate()
    {
        RandomMoviment();
        ChangeEnemyColor();
        DestroyEnemy();
    }

    float increaseTime;

    void RandomMoviment()
    {
        int num = Random.Range(1, 5);
        switch (num)
        {
            case 1:
                enemyRigibody.AddForce(Vector2.down * enemyVelocity);
                break;
            case 2:
                enemyRigibody.AddForce(Vector2.up * enemyVelocity);
                break;
            case 3:
                enemyRigibody.AddForce(Vector2.right * enemyVelocity);
                break;
            case 4:
                enemyRigibody.AddForce(Vector2.left * enemyVelocity);
                break;
        }
    }
    void ChangeEnemyColor()
    {
        if (enemyRigibody.velocity.magnitude <= 0.05f)
        {
            time += Time.deltaTime;
            if (time > 1.5f)
            {
                enemySprite.color = colors[1];
            }
        }
        else
        {
            time = 0;
            enemySprite.color = colors[0];
        }
    }
    public float timeToDestroy;

    void DestroyEnemy()
    {
        if (time > timeToDestroy)
        {
            gm.Score();

            Vector3 particlePos = gameObject.transform.position;
            particle.transform.position = particlePos;
            particle.Play();

            Creator.isAddingLine = true;
            TimeLimited.addTime = true;

            gm.enemyCount--;

            Debug.Log(gm.enemyCount);

            Destroy(gameObject);
        }
    }
}
