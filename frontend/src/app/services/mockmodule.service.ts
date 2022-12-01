import {Injectable} from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export class ModuleService {
    MockModule1 = this.createModule("MockModule1", 1, [], [], "This is just a mock");
    //ModuleList: Module[] = [];
    public MockModuleList: string[] = ["MockModule1", "MockModule2", "MockModule3", "MockModule4"];
    

    constructor () {
    };
    createModule(mName: string, id: number, users: number[], cpts: Chapter[], desc: string){
        let cName = new Module;
        cName.Init(mName, id, users, cpts, desc);
        return cName
    }
    createChapter() {

    }
    searchModulebyID() {

    }
}
      //datastructure for modules
class Module {
    Init(name: string, id: number, users: number[], cpts: Chapter[], desc: string) {
        this.moduleName=name;
        this.moduleID=id;
        this.responsibleUsers=users;
        this.chapters=cpts;
        this.moduleDescription= desc;
    };
    addChapter(cpt: Chapter){
        this.chapters.push(cpt);
        cpt.module=this.moduleID;
    };
    moduleName: string = "";
    moduleID: number = 0;           //IDs start with 1 so 0 shows this module is not active
    responsibleUsers: number[] = [];
    chapters: Chapter[] = [];
    moduleDescription: string = "";
}
  
class Chapter {
    Init(){

    }
    addExercise(ex: Exercise){
        this.exercises.push(ex);
    };
    module: number = 0;             //->inactive till module is assigned
    chapterName: string ="";
    chapterDescription: string="";
    exercises: Exercise[]=[];
}
  
class Exercise {    
    exerciseID: number=0;           //IDs start with 1 so 0 shows this exercise is not active
    exerciseName: string="";
    exerciseType: exerciseTypes=exerciseTypes["Syntax"];
}
  
enum exerciseTypes {
    "Syntax",
    "Parson",
    "TextAnswer",
    "Coding",
    "FindBug"
}
  