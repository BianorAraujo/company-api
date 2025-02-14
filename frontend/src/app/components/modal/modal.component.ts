import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CompanyService } from 'src/app/services/company.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent {
  @Input() item: any;
  @Output() save = new EventEmitter<any>();
  @Output() close = new EventEmitter<void>();
  @Input() titleModal!: string;

  constructor(private companyService: CompanyService) {}

  response: any = null;
  hasError: boolean = false;
  errorMessage: string = '';

  onSave() {
    if (this.item.id === 0) {
      this.createCompany();
    } else {
      this.updateCompany();
    }
    
  }

  updateCompany() {
    this.companyService.UpdateCompany(this.item).subscribe({
      next: (data) => {
        this.response = this.item;

        this.hasError = false;
        this.errorMessage = '';

        this.save.emit(this.response);
        this.closeModal();
      },
      error: (error) => {
        this.hasError = true;

        if (error.error?.errors) {
          const errors = error.error.errors;

          this.errorMessage = Object.values(errors).flat().join('\n');
          
        } else if (error.error?.error) {
          this.errorMessage = error.error.error;
        }
      }
    });
  }

  createCompany(){
    this.companyService.CreateCompany(this.item).subscribe({
      next: (data) => {
        this.item.id = data;
        this.response = this.item;

        this.hasError = false;
        this.errorMessage = '';

        this.save.emit(this.response);
        this.closeModal();
      },
      error: (error) => {
        this.hasError = true;

        if (error.error?.errors) {
          const errors = error.error.errors;

          this.errorMessage = Object.values(errors).flat().join('\n');
          
        } else if (error.error?.error) {
          this.errorMessage = error.error.error;
        }    
      }
    });
  }

  closeModal() {
    this.close.emit();
  }
}
