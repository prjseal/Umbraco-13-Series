Author: Louis Ferreira
Date: 08/08/2024

Changes made:
1. Refactor code into seperate projects
2. Added Razor Class Library and moved all App_Plugins files to that project (keeps umbraco project clean)
3. Added new templates for:
	RedirectToParent - allows the node link to send editor to parent (e.g. Reuseable Content node)
	RedirectToRoot - allows the node link to send editor to home/root (e.g. Settings node)
4. Changed ModelBuilder to output to Shared project, and generate on build
5. Added launchUrl to start with Umbraco back office when launched from VS
6. Moved .editorConfig file to root so it applies to all projects in solution

Further possible enhancements:
1. Create a template project from this solution so that this can easily be re-created using 'dotnet new <template-name>' or installed via Nuget
2. Parametise the project template from #1 to switch on features when project is created (e.g. add Azure Blob Storage, or OpenID etc)
3. Use ViewComponents instead of Partialviews - allows more flexibility with views (especially re-useable components)