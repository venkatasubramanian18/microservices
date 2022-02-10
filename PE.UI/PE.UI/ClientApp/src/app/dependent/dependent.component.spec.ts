import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { dependentComponent } from './dependent.component';

describe('dependentComponent', () => {
  let component: dependentComponent;
  let fixture: ComponentFixture<dependentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ dependentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(dependentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
