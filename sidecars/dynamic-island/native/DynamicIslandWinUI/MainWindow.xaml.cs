using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

namespace DynamicIslandWinUI;

public class ProjectItem
{
    public string Name { get; set; } = "";
    public string FullPath { get; set; } = "";
}

public class SkillItem
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Icon { get; set; } = "⚡";
    public string Command { get; set; } = "";
}

public class SessionItem
{
    public string Title { get; set; } = "";
    public string Preview { get; set; } = "";
    public string StatusText { get; set; } = "IDLE";
    public Brush StatusBg { get; set; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0x1F, 0x80, 0x80, 0x80));
    public Brush StatusColor { get; set; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x8E, 0x8E, 0x9A));
    public string StepCountInfo { get; set; } = "";
}

public class ToolItem
{
    public string Icon { get; set; } = "💻";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string Command { get; set; } = "";
}

public sealed partial class MainWindow : Window
{
    private ObservableCollection<ProjectItem> _projects = new();
    private ObservableCollection<SkillItem> _skills = new();
    private ObservableCollection<SessionItem> _sessions = new();
    private ObservableCollection<ToolItem> _tools = new();
    private string _currentProject = "AppWatch";

    public MainWindow()
    {
        InitializeComponent();
        AppWindow.Resize(new Windows.Graphics.SizeInt32(960, 680));
        
        LoadData();
    }

    private void LoadData()
    {
        // 1. Projects
        _projects.Add(new ProjectItem { Name = "AppWatch", FullPath = @"F:/Development/Dev/mydev/AppWatch" });
        _projects.Add(new ProjectItem { Name = "PhotoBuild", FullPath = @"F:/Development/Dev/mydev/PhotoBuild" });
        _projects.Add(new ProjectItem { Name = "zapret-simple", FullPath = @"F:/Development/Dev/mydev/zapret-simple" });
        _projects.Add(new ProjectItem { Name = "FileSorter", FullPath = @"F:/Development/Dev/mydev/PyPRJ/FileSorter" });
        _projects.Add(new ProjectItem { Name = "skllfi-suite", FullPath = @"C:/Users/dexox/.gemini/config/plugins/skllfi-suite" });
        ProjectsListView.ItemsSource = _projects;

        // 2. Skills
        _skills.Add(new SkillItem { Name = "Security Audit", Description = "Полный аудит уязвимостей и границ доверия проекта", Icon = "🛡️", Command = "Проведи полный security-audit проекта на уязвимости" });
        _skills.Add(new SkillItem { Name = "Unit Test Generator", Description = "Комплексные тесты и изоляция edge cases", Icon = "🧪", Command = "Сгенерируй надежные unit-тесты (unit-testing-test-generate)" });
        _skills.Add(new SkillItem { Name = "Impeccable UI", Description = "UX аудит, дизайн-токены, полировка контраста и анимаций", Icon = "✨", Command = "Выполни полировку и аудит интерфейса по правилам impeccable" });
        _skills.Add(new SkillItem { Name = "Project Memory", Description = "Дистилляция архитектурных решений и инвариантов", Icon = "🧠", Command = "Синхронизируй проектную память по правилам project-memory" });
        _skills.Add(new SkillItem { Name = "Repo Roadmap", Description = "Трекер бэклога: Now / Next / Shipped", Icon = "🗺️", Command = "Обнови roadmap проекта по навыку repo-roadmap" });
        SkillsListView.ItemsSource = _skills;

        // 3. Sessions
        LoadSessions();

        // 4. Tools
        _tools.Add(new ToolItem { Title = "Windows Terminal", Subtitle = "PowerShell 7", Icon = "💻", Command = "wt.exe" });
        _tools.Add(new ToolItem { Title = "VS Code", Subtitle = "Редактор кода", Icon = "🟦", Command = "code" });
        _tools.Add(new ToolItem { Title = "Live Web Preview", Subtitle = "http://localhost:5173", Icon = "🌐", Command = "http://localhost:5173" });
        _tools.Add(new ToolItem { Title = "Antigravity Brain", Subtitle = "Артефакты и кэши", Icon = "📁", Command = @"explorer.exe C:\Users\dexox\.gemini\antigravity" });
        _tools.Add(new ToolItem { Title = "Git Status", Subtitle = "Проверка репозитория", Icon = "🌿", Command = "cmd.exe /c git status & pause" });
        _tools.Add(new ToolItem { Title = "skllfi-suite Core", Subtitle = "Папка мета-плагина", Icon = "🧩", Command = @"explorer.exe C:\Users\dexox\.gemini\config\plugins\skllfi-suite" });
        ToolsGridView.ItemsSource = _tools;

        AppendLog("[02:35] AppWatch WinUI 3 Native Environment инициализирован.");
        AppendLog("[02:35] Аппаратный MicaBackdrop активен.");
        AppendLog("[02:35] Синхронизировано 5 проектов Antigravity.");
    }

    private void LoadSessions()
    {
        _sessions.Clear();
        _sessions.Add(new SessionItem
        {
            Title = "AppWatch WinUI 3 & Deep Antigravity Integration",
            Preview = "что можешь сказать про https://www.sharrowkin.dev/?",
            StatusText = "RUNNING",
            StatusBg = new SolidColorBrush(Windows.UI.Color.FromArgb(0x1F, 0x2E, 0xA0, 0x43)),
            StatusColor = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x2E, 0xA0, 0x43)),
            StepCountInfo = "Шагов: 755 • Текущая активная сессия"
        });
        _sessions.Add(new SessionItem
        {
            Title = "PhotoBuild - DP-MFC Red OS 8 Specification",
            Preview = "приложение для Red OS 8, аналог DPStudio. Проверка ТЗ и прототипа...",
            StatusText = "IDLE",
            StatusBg = new SolidColorBrush(Windows.UI.Color.FromArgb(0x1F, 0x80, 0x80, 0x80)),
            StatusColor = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x8E, 0x8E, 0x9A)),
            StepCountInfo = "Шагов: 131 • Завершено"
        });
        SessionsListView.ItemsSource = _sessions;
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem item && item.Tag is string tag)
        {
            ViewActivity.Visibility = (tag == "activity") ? Visibility.Visible : Visibility.Collapsed;
            ViewProjects.Visibility = (tag == "projects") ? Visibility.Visible : Visibility.Collapsed;
            ViewSkills.Visibility = (tag == "skills") ? Visibility.Visible : Visibility.Collapsed;
            ViewSessions.Visibility = (tag == "sessions") ? Visibility.Visible : Visibility.Collapsed;
            ViewTools.Visibility = (tag == "tools") ? Visibility.Visible : Visibility.Collapsed;
            ViewDiffs.Visibility = (tag == "diffs") ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    private void BtnSendCmd_Click(object sender, RoutedEventArgs e)
    {
        SendPrompt();
    }

    private void CmdBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
        {
            SendPrompt();
        }
    }

    private void SendPrompt()
    {
        string text = CmdBox.Text.Trim();
        if (string.IsNullOrEmpty(text)) return;

        AppendLog($"⚡ Отправлена команда агенту: {text}");
        CmdBox.Text = "";
    }

    private void BtnRollback_Click(object sender, RoutedEventArgs e)
    {
        AppendLog("↺ [SAFE ROLLBACK] Чекпоинт подтвержден. Изменения кода отменены.");
    }

    private void BtnSelectProject_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string path)
        {
            _currentProject = Path.GetFileName(path.TrimEnd('/', '\\'));
            TxtActiveProject.Text = $"{_currentProject} (active)";
            AppendLog($"📁 Рабочий проект переключен на: {_currentProject}");
            NavView.SelectedItem = NavItemActivity;
        }
    }

    private void ProjectsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProjectsListView.SelectedItem is ProjectItem proj)
        {
            _currentProject = proj.Name;
            TxtActiveProject.Text = $"{_currentProject} (active)";
            AppendLog($"📁 Рабочий проект переключен на: {_currentProject}");
        }
    }

    private void BtnRunSkill_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string cmd)
        {
            AppendLog($"🧠 Запущен Agent Skill для '{_currentProject}': {cmd}");
            NavView.SelectedItem = NavItemActivity;
        }
    }

    private void BtnRefreshSessions_Click(object sender, RoutedEventArgs e)
    {
        LoadSessions();
        AppendLog("↻ Сессии Antigravity синхронизированы.");
    }

    private void BtnLaunchTool_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string cmd)
        {
            try
            {
                Process.Start(new ProcessStartInfo(cmd) { UseShellExecute = true });
                AppendLog($"🚀 Запущен инструмент: {cmd}");
            }
            catch (Exception ex)
            {
                AppendLog($"⚠ Ошибка запуска: {ex.Message}");
            }
        }
    }

    private void BtnAddFolder_Click(object sender, RoutedEventArgs e)
    {
        AppendLog("📁 Диалог добавления папки проекта вызван.");
    }

    private void BtnAddTool_Click(object sender, RoutedEventArgs e)
    {
        AppendLog("⚡ Диалог добавления инструмента вызван.");
    }

    private void AppendLog(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        TerminalText.Text = $"[{timestamp}] {message}\n" + TerminalText.Text;
    }
}
