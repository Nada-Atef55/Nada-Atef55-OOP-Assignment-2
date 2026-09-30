public class Solution {
    public int MaxVowels(string s, int k) {
        char[] vowels={'a', 'e', 'i', 'o', 'u'};
        int count=0;
        for(int i=0; i<k;i++){
            if(vowels.Contains(s[i])){
                count++;
            }
        }

        int max=count;
        int l=1, r=k;
        while(r<s.Length){
            if(vowels.Contains(s[l-1])){
                count--;
            }
            if(vowels.Contains(s[r])){
                count++;
            }
            max=Math.Max(max, count);
            l++;
            r++;
        }
        return max;
    }
}