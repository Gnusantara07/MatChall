using UnityEngine;
using TMPro; // Untuk menggunakan TextMesh Pro
using UnityEngine.UI; // Untuk menggunakan UI Button dan Slider
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Referensi ke TextMeshPro untuk pertanyaan
    public Button trueButton; // Referensi ke tombol "Benar"
    public Button falseButton; // Referensi ke tombol "Salah"
    public TextMeshProUGUI resultText; // Referensi ke TextMeshPro untuk hasil
    public TextMeshProUGUI scoreText; // Referensi ke TextMeshPro untuk skor
    public Slider healthBar; // Referensi ke UI Slider untuk health bar

    private int currentQuestionIndex = 0; // Indeks pertanyaan saat ini
    private int score = 0; // Variabel untuk menyimpan skor
    private int health = 5; // Variabel untuk menyimpan health

    // Struktur data untuk menyimpan pertanyaan dan jawaban
    [System.Serializable]
    public struct Question
    {
        public string questionText;
        public bool correctAnswer;
    }

    // Array untuk menyimpan semua pertanyaan
    public Question[] questions;

    void Start()
    {
        // Pastikan ada pertanyaan yang diset
        if (questions.Length > 0)
        {
            // Tampilkan pertanyaan pertama
            DisplayQuestion();
        }

        // Tambahkan listener ke tombol
        trueButton.onClick.AddListener(() => CheckAnswer(true));
        falseButton.onClick.AddListener(() => CheckAnswer(false));

        // Sembunyikan teks hasil di awal
        resultText.gameObject.SetActive(false);

        // Tampilkan skor dan health awal
        UpdateScoreText();
        UpdateHealthBar();
    }

    void DisplayQuestion()
    {
        // Tampilkan pertanyaan saat ini
        questionText.text = questions[currentQuestionIndex].questionText;
    }

    void CheckAnswer(bool playerAnswer)
    {
        // Cek apakah jawaban pemain benar atau salah
        if (playerAnswer == questions[currentQuestionIndex].correctAnswer)
        {
            resultText.text = "Jawaban Benar!";
            score++; // Tambah skor jika jawaban benar
            UpdateScoreText(); // Perbarui teks skor
        }
        else
        {
            resultText.text = "Jawaban Salah!";
            health--; // Kurangi health jika jawaban salah
            UpdateHealthBar(); // Perbarui health bar
        }

        // Tampilkan teks hasil
        resultText.gameObject.SetActive(true);

        // Tampilkan soal berikutnya setelah beberapa detik
        Invoke("NextQuestion", 2.0f); // Ganti 2.0f dengan waktu delay yang diinginkan
    }

    void NextQuestion()
    {
        // Sembunyikan teks hasil
        resultText.gameObject.SetActive(false);

        // Cek apakah health habis
        if (health <= 0)
        {
            questionText.text = "Kamu Kalah!";
            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);
            return;
        }

        // Naikkan indeks pertanyaan
        currentQuestionIndex++;

        // Jika masih ada pertanyaan, tampilkan pertanyaan berikutnya
        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            // Jika tidak ada lagi pertanyaan, pindah ke scene berikutnya atau tampilkan hasil akhir
            questionText.text = "Kuis selesai!";
            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Skor: " + score;
    }

    void UpdateHealthBar()
    {
        healthBar.value = (float)health / 5f; // Asumsikan health maksimum adalah 5
    }

    // Fungsi untuk memuat halaman berikutnya (opsional)
    void LoadNextScene()
    {
        // Ganti "NextSceneName" dengan nama scene yang ingin Anda muat
        SceneManager.LoadScene("NextSceneName");
    }
}
