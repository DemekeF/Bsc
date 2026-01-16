import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BscKpiComponent } from './bsc-kpi.component';

describe('BscKpiComponent', () => {
  let component: BscKpiComponent;
  let fixture: ComponentFixture<BscKpiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BscKpiComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BscKpiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
