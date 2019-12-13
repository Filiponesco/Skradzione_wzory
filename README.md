# Skradzione_wzory  
## BRANCH: CosineDistance
 Semestr V inżynieria oprogramowania - projekt
  
  # Funkcje do wykorzystania
### 1. CosineDistance(string wzorOryginalny, string wzorSkopiowany)
 - przetwarza wzór, i tworzy listy dla oryginału i kopii Dictionary<char, int> aby poznać ile razy powtórzył się dany znak we wzorze.
 - znak, który występuje w kopii, a nie występuje w oryginale jest usuwany
 - znak, który występuje w oryginale, a nie występuje w kopii dodawany jest do listy wzoru skopiowanego i przyjmuje wartość 0
 - listy Dictionary<char, int> wzorOryg oraz Dictionary<char, int> wzorCopy są wykorzystywane w algorytmie "cosine distance",
 - zwraca wartość, w przedziale [0; 1] im mniejsza liczba tym wzór oryginalny jest bardziej podobny skopiowanego

# Źródła
- tłumaczy co to cosine similarity(hinduski): https://www.youtube.com/watch?v=xY3jrJdpuQg
- **blog wyjaśnia cosine similarity:** https://www.machinelearningplus.com/nlp/cosine-similarity/
- zapis wzorów w Latex: http://www.latex-kurs.x25.pl/paper/tryb_matematyczny
- https://stackoverflow.com/questions/1746501/can-someone-give-an-example-of-cosine-similarity-in-a-very-simple-graphical-wa
- różnica między cosine similarity, a cosine distance: https://www.itl.nist.gov/div898/software/dataplot/refman2/auxillar/cosdist.htm
