import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'valueToEnumString',
  standalone: true
})
export class ValueToEnumStringPipe implements PipeTransform {

  transform(value: number, _enum: any): any {
    return Object.values(_enum)[value];;
  }

}
