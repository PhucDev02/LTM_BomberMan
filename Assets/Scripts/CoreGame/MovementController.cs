using System.Net.Sockets;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class MovementController : MonoBehaviour
    {
        public PlayerClientController controller;
        private new Rigidbody2D rigidbody;
        private Vector2 direction = Vector2.down;
        [Space(10)]
        public float speed = 5f;

        [Header("Input")]
        public FixedJoystick joystick;
        [Header("Sprites")]
        public AnimatedSpriteRenderer spriteRendererUp;
        public AnimatedSpriteRenderer spriteRendererDown;
        public AnimatedSpriteRenderer spriteRendererLeft;
        public AnimatedSpriteRenderer spriteRendererRight;
        public AnimatedSpriteRenderer spriteRendererDeath;
        private AnimatedSpriteRenderer activeSpriteRenderer;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            activeSpriteRenderer = spriteRendererDown;
            joystick = FindObjectOfType<FixedJoystick>();

        }

        private void Update()
        {
            if (joystick.Direction.y > 0)
            {
                SetDirection(Vector2.up, spriteRendererUp);
            }
            if (joystick.Direction.y < 0)
            {
                SetDirection(Vector2.down, spriteRendererDown);
            }
            if (joystick.Direction.x < 0)
            {
                SetDirection(Vector2.left, spriteRendererLeft);
            }
            if (joystick.Direction.x > 0)
            {
                SetDirection(Vector2.right, spriteRendererRight);
            }
            if (joystick.Direction == Vector2.zero)
            {
                SetDirection(Vector2.zero, activeSpriteRenderer);
            }

        }

        private void FixedUpdate()
        {
            Vector2 position = rigidbody.position;
            Vector2 translation = direction * speed * Time.fixedDeltaTime;

            rigidbody.MovePosition(position + translation);
            controller.SetPosition(rigidbody.position);
        }

        private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
        {
            direction = newDirection;

            spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
            spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
            spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
            spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

            activeSpriteRenderer = spriteRenderer;
            activeSpriteRenderer.idle = direction == Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
            {
                DeathSequence();
            }
        }

        private void DeathSequence()
        {
            enabled = false;
            GetComponent<BombController>().enabled = false;

            spriteRendererUp.enabled = false;
            spriteRendererDown.enabled = false;
            spriteRendererLeft.enabled = false;
            spriteRendererRight.enabled = false;
            spriteRendererDeath.enabled = true;

            Invoke(nameof(OnDeathSequenceEnded), 1.25f);
        }

        private void OnDeathSequenceEnded()
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().CheckWinState();
        }
        private void OnValidate()
        {
            controller = GetComponent<PlayerClientController>();
        }
    }
}