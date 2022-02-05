import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowBenefitReportComponent } from './show-benefit-report.component';

describe('ShowBenefitReportComponent', () => {
  let component: ShowBenefitReportComponent;
  let fixture: ComponentFixture<ShowBenefitReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowBenefitReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowBenefitReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
