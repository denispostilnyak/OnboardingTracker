import { ChangeDetectorRef, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { BehaviorSubject, Subject } from 'rxjs';
import { last } from 'rxjs/operators';
import { Breadcrumb } from 'src/app/models/common/breadcrumb';
import { BreadcrumbsService } from 'src/app/services/breadcrumbs/breadcrumbs.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
@Component({
  selector: 'app-breadcrumbs',
  templateUrl: './breadcrumbs.component.html',
  styleUrls: ['./breadcrumbs.component.scss'],
})
export class BreadcrumbsComponent implements OnInit {

  private location!: string;

  breadcrumbs = new Subject<Breadcrumb[]>();
  lastBreadcrumb = new Subject<Breadcrumb>();
  display = new Subject<boolean>();

  constructor(
    private bcService: BreadcrumbsService,
    private notificationService: NotificationService,
  ) {}

  ngOnInit(): void {
    this.bcService.location$.subscribe(location=>{
      if(location=="/"){
        this.display.next(false);
        this.location = location;
      }else{
        this.display.next(true);
        this.location = location;
      }
    });
    this.bcService.breadcrumbs$.subscribe((breadcrumbs) => {
        this.display.next(breadcrumbs.length != 1);
        this.breadcrumbs.next(breadcrumbs);
        this.lastBreadcrumb.next(breadcrumbs.slice(-1)[0]);
    });
  }

  pushClick(){
    this.notificationService.click$.next(this.location);
  }
}
