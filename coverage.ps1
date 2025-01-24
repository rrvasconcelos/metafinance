# Define pasta raiz dos testes
$testsRoot = Join-Path $PSScriptRoot "tests"

# Encontra todos os projetos de teste
$testProjects = Get-ChildItem -Path $testsRoot -Directory

foreach ($testProject in $testProjects) {
    Write-Host "`nExecutando testes para: $($testProject.Name)" -ForegroundColor Cyan
    
    $coveragePath = Join-Path $testProject.FullName "coverage"
    $reportPath = Join-Path $testProject.FullName "coveragereport"

    # Limpa e cria diretório de cobertura
    Remove-Item -Path $coveragePath, $reportPath -Recurse -Force -ErrorAction SilentlyContinue
    New-Item -ItemType Directory -Force -Path $coveragePath | Out-Null

    # Executa testes
    dotnet test $testProject.FullName /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput="$coveragePath\"
    
    # Adiciona arquivo de cobertura à lista
    $coverageFile = Get-ChildItem -Path $coveragePath -Filter "coverage.cobertura.xml" -Recurse | Select-Object -First 1
    if ($coverageFile) {
        # Gera relatório para o projeto atual
        reportgenerator -reports:"$($coverageFile.FullName)" -targetdir:"$reportPath" -reporttypes:Html
        Invoke-Item (Join-Path $reportPath "index.html")
    }
}
