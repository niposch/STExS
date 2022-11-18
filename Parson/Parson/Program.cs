using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parson
{
    class Stringular //: BaseExercise
    {
        
        private String snippet;
        private int index;

        public String Snippet{
            get{return snippet;}
        }
        public int Index{
            get{return index;}
}
        public Stringular(String code, int index) {
            this.snippet=code;
            this.index=index;
        }


     }

    class Parson //:BaseExercise
        {
        String originalCode;
        private List<Stringular> puzzle;

        public Parson(String input){
            this.originalCode=input;
            this.puzzle=new List<Stringular>();
            this.blocksize();
        }

        private int countLines() {
            int linecount=0;
            for (int i = 0; i < originalCode.Length; i++) {
                if (originalCode[i] == '\n')
                    linecount++;
            }
            return linecount;
        }

        private void blocksize() {
            int lines = countLines();
            int blocksize;
            if (lines <= 4)
                blocksize = 1;
            else if (lines <= 10)
                blocksize = 2;
            else if (lines <= 21)
                blocksize = 3;
            else
                blocksize = 5;

            split(blocksize);
        }

        private void split(int blocksize) {
            int counter = 0;
            int currentBlock=0;
            int startindex = 0;
            for (int i = 0; i < this.originalCode.Length; i++) {
                if (this.originalCode[i] == '\n') {
                    counter++;
                    if (counter == blocksize) {
                        
                        Stringular temp= new Stringular(this.originalCode.Substring(startindex, i - startindex), currentBlock);
                        this.puzzle.Add(temp);
                        counter = 0;
                        startindex = i+1;
                        currentBlock++;
                        
               
                    }
                   
                }
                 if(i==(this.originalCode.Length-1)){
                        Stringular temp= new Stringular(this.originalCode.Substring(startindex), currentBlock);
                        this.puzzle.Add(temp);
                    }
            }
        }

        public void shuffle(){
         Random rng = new Random();
         int n= this.puzzle.Count;
            while (n>1){
                n--;
                int k = rng.Next(n+1);
                Stringular temp= puzzle[k];
                puzzle[k] =puzzle[n];
                puzzle[n]=temp;
            }
        }

        public int finalScore(int maxScore){
            int correctBlocks=0;
            for(int i=0; i<this.puzzle.Count; i++){
            if(i==this.puzzle[i].Index)
                    correctBlocks++;
            }
            float achievedPoints= (float)correctBlocks/this.puzzle.Count;
            return (int) (maxScore*achievedPoints+0.5f);
        }

        public void showContent(){
        for(int i=0; i<this.puzzle.Count; i++){
                Console.WriteLine(this.puzzle[i].Index+" "+this.puzzle[i].Snippet );
            }
        }
        /*
        static void Main(string[] args)
        {
            String input = "line1\nline2\nline3 \nline4\nline5 \nline6 \nline7";
            Parson test = new Parson(input);
            test.showContent();
            test.shuffle();
            Console.WriteLine();
            test.showContent();
            Console.WriteLine("\n Score:"+test.finalScore(10));
             test.shuffle();
            Console.WriteLine();
            test.showContent();
            Console.Read();

        }
        */
    }
}
