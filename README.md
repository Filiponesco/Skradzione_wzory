# Skradzione_wzory
 Semestr V inżynieria oprogramowania - projekt
 
# Testy
## Baza danych
Baza testowa składająca się z 10 plików oryginalnych oraz po 5 kopii dla każdego oryginału (w sumie 50 kopii) wyznaczyła następujące maksymalne wartości dla algorytmu "Euclidean distance":
 - 1: 23,72762
 - 2: 17,77639
 - 3: 12,04160
 - 4: 16,34014
 - 5: 16,58312
 - 6: 16,64332
 - 7: 17,34935
 - 8: 44,65423 <-- max
 - 9: 11,35783
 - 10: 15,19868

Wartość maksymalna została przypisana do zmiennej "maxOfEuclidean" w klasie: "Algorytm.cs"

## Algorytmy: 
### Cosine distance
 Po wprowadzeniu identycznych danych jak na stronie internetowej: https://neo4j.com/docs/graph-algorithms/current/labs-algorithms/cosine/
 otrzymaliśmy identyczny wynik.
 
 #### Strona internetowa
![alt text](https://github.com/Filiponesco/Skradzione_wzory/blob/master/zrzut_strony.JPG)

#### Nasz wynik
![alt text](https://github.com/Filiponesco/Skradzione_wzory/blob/master/zrzut_raportu_html.JPG)


### Euclidean distance

 #### Strona internetowa
![alt text](https://github.com/Filiponesco/Skradzione_wzory/blob/master/euclidean_source.JPG)

#### Nasz wynik
![alt text](https://github.com/Filiponesco/Skradzione_wzory/blob/master/html_euclidean.JPG)

Nasz wynik wynosi: 80,9978148228733, jest to procent podobieństwa. Sam algorytm euclidean distance zwraca dystans z przedziału od 0 do nieskończoności. Wyznaczyliśmy maksymalną granicę poprzez "przepuszczenie" algorytmu przez cała bazę danych. MaxOfEuclidean wynosi dla naszej bazy 44.65423.
Procent podobieństwa wyznaczony jest wg następującego kodu: 

lb - wartość zwrócona przez algorytm

```
double pom = (double) (maxOfEuclidean - lb);
result = Scale(pom, 0.0, maxOfEuclidean, 0.0, 1.0);

private static double Scale(double value, double min, double max, double toMin, double toMax)
{
    //y=mx+c
    double result = (value - min) / (max - min) * (toMax - toMin) + toMin;
    return result;
    }
```
Na końcu rezultat mnożony jest razy 100.

pom = 44.65423 - 8.485 = 36.16923   
result = (36.16923 - 0) / (44.65423 - 0) * (1 - 0) + 0   
result = 0.80998   

Zatem wynik jest poprawny.

# Źródła
- tłumaczy co to cosine similarity(hinduski): https://www.youtube.com/watch?v=xY3jrJdpuQg
- **blog wyjaśnia cosine similarity:** https://www.machinelearningplus.com/nlp/cosine-similarity/
- zapis wzorów w Latex: http://www.latex-kurs.x25.pl/paper/tryb_matematyczny
- https://stackoverflow.com/questions/1746501/can-someone-give-an-example-of-cosine-similarity-in-a-very-simple-graphical-wa
- różnica między cosine similarity, a cosine distance: https://www.itl.nist.gov/div898/software/dataplot/refman2/auxillar/cosdist.htm
