# Skradzione_wzory  
## BRANCH: CosineDistance
 Semestr V inżynieria oprogramowania - projekt
  
  # Funkcje do wykorzystania
### 1. CountLines(String path)
 - liczy w pliku tekstowym liczbę powtórzeń wszystkich liń. Np.:
 >a     
 >b  
 >b   
 >c    
 >c  
 >c 
  
 - Zwróci **Key:** ["a"], **Value:** 1; **Key:** ["b"], **Value:** 2; **Key:** ["c"], **Value:** 3  
 - Dictionary<String, int> to lista, której indexami(keys) jest string, int to value.  
   
### 2. ToArray(Dictionary<String, int> orig, List<Dictionary<String, int>> copyFiles)
 - zamienia Dictionary<String, int> original oraz List<Dictionary<String, int>> copyFiles w tablicę dwuwymiarową.
 - Original to liczba powtórzeń wszystkich liń w oryginalnym pliku.
 - copyFiles to lista liczby powtórzeń liń dla każdego pliku skopiowanego
 - Zwraca tablicę dwywymiarową [lb. dokumentów, lb. różnych liń w oryginale], wartościami tej tablicy jest częstość występnowania lini w danym dokumencie.
  ![alt text](https://github.com/Filiponesco/Skradzione_wzory/blob/cosDist/array.jpg)
# Źródła
- tłumaczy co to cosine similarity(hinduski): https://www.youtube.com/watch?v=xY3jrJdpuQg
- **blog wyjaśnia cosine similarity:** https://www.machinelearningplus.com/nlp/cosine-similarity/
- zapis wzorów w Latex: http://www.latex-kurs.x25.pl/paper/tryb_matematyczny
- https://stackoverflow.com/questions/1746501/can-someone-give-an-example-of-cosine-similarity-in-a-very-simple-graphical-wa
- różnica między cosine similarity, a cosine distance: https://www.itl.nist.gov/div898/software/dataplot/refman2/auxillar/cosdist.htm
