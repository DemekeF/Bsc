import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BscComponentComponent } from './bsc-component.component';

describe('BscComponentComponent', () => {
  let component: BscComponentComponent;
  let fixture: ComponentFixture<BscComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BscComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BscComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
