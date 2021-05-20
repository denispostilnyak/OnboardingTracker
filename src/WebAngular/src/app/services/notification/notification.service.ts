import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  public click$ = new Subject<string>();
  private callbacks : { [id: string] : {object:any,callback: ()=>void;} } = {};
  public registerCallbackFor(url:string, object: any, callback: ()=>void){
    this.callbacks[url]={object, callback};
    console.log(this.callbacks[url]);
  }
  constructor() {
    this.click$.subscribe(url=>{
      console.log("got new callback at url ", url);
      let callback = this.callbacks[url].callback;
      callback.call(this.callbacks[url].object);
    });
  }


}
