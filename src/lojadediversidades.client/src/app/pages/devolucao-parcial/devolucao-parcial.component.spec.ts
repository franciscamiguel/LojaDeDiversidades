import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DevolucaoParcialComponent } from './devolucao-parcial.component';


describe('DevolucoesComponent', () => {
  let component: DevolucaoParcialComponent;
  let fixture: ComponentFixture<DevolucaoParcialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DevolucaoParcialComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DevolucaoParcialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
