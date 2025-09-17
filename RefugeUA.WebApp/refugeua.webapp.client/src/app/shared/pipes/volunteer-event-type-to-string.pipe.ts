import { Pipe, PipeTransform } from '@angular/core';
import { VolunteerEventType } from '../../core/enums/volunteer-event-type-enum';
import { IsValueNullNanOrUndefined } from '../util/value-not-null-undefined-or-nan-checker';

@Pipe({
  name: 'volunteerEventTypeToString'
})
export class VolunteerEventTypeToStringPipe implements PipeTransform {
  transform(value: VolunteerEventType | undefined): string {

    switch (value) {
      case VolunteerEventType.Participation:
        return 'Пряма участь';
      case VolunteerEventType.Donation:
        return 'Пожертвування';
      default:
        return 'Unknown';
    }
  }
}
