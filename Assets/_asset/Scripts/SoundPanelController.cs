using UnityEngine;
using UnityEngine.UI;

public class SoundPanelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private VolumeSettings volumeSettings; // Script VolumeSettings hiện có

    [Header("SFX Settings")]
    [SerializeField] private GameObject sfxPanel;     // Panel chứa SFX slider
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Image sfxOnIcon;
    [SerializeField] private Image sfxOffIcon;

    [Header("Music Settings")]
    [SerializeField] private GameObject musicPanel;   // Panel chứa Music slider
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Image musicOnIcon;
    [SerializeField] private Image musicOffIcon;

    [Header("Timing")]
    [SerializeField] private float hideDelay = 1f;    // 1 giây tự ẩn

    private float sfxHideTimer;
    private float musicHideTimer;
    private bool sfxAdjusting;
    private bool musicAdjusting;

    private void Start()
    {
        sfxPanel.SetActive(false);
        musicPanel.SetActive(false);

        sfxSlider.onValueChanged.AddListener(OnSfxChanged);
        musicSlider.onValueChanged.AddListener(OnMusicChanged);

        UpdateIcons();
    }

    private void Update()
    {
        // Kiểm tra panel SFX
        if (sfxPanel.activeSelf && !sfxAdjusting)
        {
            sfxHideTimer -= Time.unscaledDeltaTime;
            if (sfxHideTimer <= 0)
                sfxPanel.SetActive(false);
        }

        // Kiểm tra panel Music
        if (musicPanel.activeSelf && !musicAdjusting)
        {
            musicHideTimer -= Time.unscaledDeltaTime;
            if (musicHideTimer <= 0)
                musicPanel.SetActive(false);
        }
    }

    // Gọi khi ấn nút SFX
    public void OnSfxButtonPressed()
    {
        sfxPanel.SetActive(true);
        sfxHideTimer = hideDelay;
    }

    // Gọi khi ấn nút Music
    public void OnMusicButtonPressed()
    {
        musicPanel.SetActive(true);
        musicHideTimer = hideDelay;
    }

    private void OnSfxChanged(float value)
    {
        sfxAdjusting = true;
        sfxHideTimer = hideDelay;
        volumeSettings.SetSfxVolume();
        UpdateIcons();

        CancelInvoke(nameof(StopSfxAdjusting));
        Invoke(nameof(StopSfxAdjusting), 0.5f);
    }

    private void OnMusicChanged(float value)
    {
        musicAdjusting = true;
        musicHideTimer = hideDelay;
        volumeSettings.SetMusicVolume();
        UpdateIcons();

        CancelInvoke(nameof(StopMusicAdjusting));
        Invoke(nameof(StopMusicAdjusting), 0.5f);
    }

    private void StopSfxAdjusting() => sfxAdjusting = false;
    private void StopMusicAdjusting() => musicAdjusting = false;

    private void UpdateIcons()
    {
        bool sfxMuted = sfxSlider.value <= 0.0001f;
        sfxOnIcon.enabled = !sfxMuted;
        sfxOffIcon.enabled = sfxMuted;

        bool musicMuted = musicSlider.value <= 0.0001f;
        musicOnIcon.enabled = !musicMuted;
        musicOffIcon.enabled = musicMuted;
    }
}
