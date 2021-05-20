import { Injectable } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { BehaviorSubject, Subject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { Breadcrumb } from 'src/app/models/common/breadcrumb';

@Injectable({
  providedIn: 'root',
})
export class BreadcrumbsService {
  public breadcrumbs$ = new Subject<Breadcrumb[]>(
  );
  public location$ = new Subject<string>();
  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.location$.next(this.router.url);
        let breadcrumbs = this.createBreadcrumbs(this.activatedRoute.root);
        this.breadcrumbs$.next(breadcrumbs);
      });
  }
  createBreadcrumbs(
    route: ActivatedRoute,
    url: string = '',
    breadcrumbs: Breadcrumb[] = []
  ): Breadcrumb[] {
    const children: ActivatedRoute[] = route.children;

    if (children.length === 0) {
      return breadcrumbs;
    }

    for (const child of children) {
      const routeURL: string = child.snapshot.url
        .map((segment) => segment.path)
        .join('/');
      if (routeURL !== '') {
        url += `/${routeURL}`;
      }

      const label = child.snapshot.data['breadcrumb'];
      const caption = child.snapshot.data['createButtonCaption'];
      if (label) {
        breadcrumbs.push({ url: url, name: label, createButtonCaption:caption });
      }

      return this.createBreadcrumbs(child, url, breadcrumbs);
    }
    return [];
  }
}
