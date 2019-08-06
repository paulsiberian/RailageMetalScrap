# RailageMetalScrap

## Клонирование
```console
git clone https://github.com/paulsiberian/RailageMetalScrap.git
cd RailageMetalScrap
```

## Запуск
```console
cd RailageMetalScrap
dotnet run [команды]
```

### Команда read
Комада считывает записи из реестров перевозок лома (файл(ы) .xlsx). Примеры:  
1) записи считываются из файлов в текущей директории:
```console
dotnet run read
```
2) записи считываются из указанного файла:
```console
dotnet run read "C://Реестр.xlsx"
```
3) записи считываются из файлов в указаной директории и указанного файла:
```console
dotnet run read "C://Dir" "C://Реестр.xlsx"
```

Параметры команды: 
1) --insert или -i -- отвечает за внесение данных в БД. Пример:  
```console
dotnet run read "C://Реестр.xlsx" -i
```
2) --display или -d -- отвечает за отображение данные в терминале. Пример:  
```console
dotnet run read "C://Реестр.xlsx" -id
```

### Команда treemap
Пока ничего не делает, но в идеале должна строить иерархическую диаграмму для выбранных данных.

## Сборка
Для Ubuntu 18.04:  
```console
dotnet build --runtime ubuntu.18.04-x64 --configuration Release
```

Для Windows 7:  
```console
dotnet build --runtime win7-x64 --configuration Release
```
