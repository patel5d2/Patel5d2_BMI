using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Patel5d2_BMI;

public partial class MainPage : ContentPage
{
    private string selectedGender = null;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnMaleTapped(object sender, EventArgs e)
    {
        selectedGender = "Male";
        MaleBorder.BorderColor = Colors.Blue; 
        FemaleBorder.BorderColor = Colors.Gray; 
    }

    private void OnFemaleTapped(object sender, EventArgs e)
    {
        selectedGender = "Female";
        FemaleBorder.BorderColor = Colors.Pink; 
        MaleBorder.BorderColor = Colors.Gray; 
    }

    private void OnHeightChanged(object sender, ValueChangedEventArgs e)
    {
        HeightLabel.Text = ((int)e.NewValue).ToString();
    }

    private void OnWeightChanged(object sender, ValueChangedEventArgs e) => WeightLabel.Text = ((int)e.NewValue).ToString();

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (selectedGender == null)
        {
            DisplayAlert("ERROR", "Please select a one gender.", "OK");
            return;
        }

        double height = HeightSlider.Value;
        double weight = WeightSlider.Value;

        if (height == 0)
        {
            DisplayAlert("ERROR", "Height cannot be zero.", "OK");
            return;
        }

        double bmi = (weight * 703) / (height * height);
        string healthStatus = GetHealthStatus(selectedGender, bmi);
        string recommendation = GetRecommendation(healthStatus);

        string message = $"Gender: {selectedGender}\nBMI: {Math.Round(bmi, 1)}\nHealth Status: {healthStatus}\nRecommendations:\n{recommendation}";
        DisplayAlert("Your calculated BMI results are:", message, "Ok");
    }

    private string GetHealthStatus(string gender, double bmi)
    {
        if (gender == "Male")
        {
            if (bmi < 18) return "Underweight";
            else if (bmi < 25) return "Normal weight";
            else if (bmi < 30) return "Overweight";
            else return "Obese";
        }
        else
        {
            if (bmi < 18) return "Underweight";
            else if (bmi < 24) return "Normal weight";
            else if (bmi < 29) return "Overweight";
            else return "Obese";
        }
    }

    private string GetRecommendation(string healthStatus)
    {
        switch (healthStatus)
        {
            case "Underweight":
                return "Increase calorie intake with nutrient-rich foods (e.g., nuts, lean protein, whole grains). Incorporate strength training to build muscle mass. Consult a nutritionist if needed.";
            case "Normal weight":
                return "Maintain a balanced diet with proteins, healthy fats, and fiber. Stay physically active with at least 150 minutes of exercise per week. Keep regular check-ups to monitor overall health.";
            case "Overweight":
                return "Reduce processed foods and focus on portion control. Engage in regular aerobic exercises (e.g., jogging, swimming) and strength training. Drink plenty of water and track your progress.";
            case "Obese":
                return "Consult a doctor for personalized guidance. Start with low-impact exercises (e.g., walking, cycling). Follow a structured weight-loss meal plan and consider behavioral therapy for lifestyle changes. Avoid sugary drinks and maintain a consistent sleep schedule.";
            default:
                return "";
        }
    }
}