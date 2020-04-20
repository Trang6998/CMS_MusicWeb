<template>
    <div class="ds-event"
         :class="classes">
        <div class="ds-event-header ds-event-area">
            <div class="ds-event-cancel" v-if="hasCancel">
                <!-- Cancel -->
                <slot name="scheduleCancel" v-bind="{cancel, labels}">
                    <v-tooltip bottom>
                        <v-btn slot="activator" icon class="ds-button" @click="cancel">
                            <v-icon dark>clear</v-icon>
                        </v-btn>
                        <span v-html="labels.cancel"></span>
                    </v-tooltip>
                </slot>
            </div>
            <div class="ds-event-actions">
                <!-- Save -->
                <slot name="scheduleSave" v-bind="{hasSave, save, labels, readOnly}">
                    <v-btn v-if="!isReadOnly"
                           class="ds-button-tall ml-3 mt-0 mb-2" depressed
                           color="primary"
                           :disabled="!canSave"
                           @click.stop="save">
                        <span v-html="labels.save"></span>
                    </v-btn>
                </slot>
                <!-- More Actions -->
                <slot name="scheduleActions" v-bind="{calendarEvent, schedule, calendar, actioned, readOnly}">
                    <ds-schedule-actions v-if="calendarEvent && !isReadOnly"
                                         v-bind="{$scopedSlots}"
                                         v-on="$listeners"
                                         :schedule="schedule"
                                         :calendar-event="calendarEvent"
                                         :calendar="calendar"
                                         @finish="actioned">
                        <v-btn class="ds-button-tall ml-1 mt-0 mb-2" depressed>
                            ----Thao tác...
                        </v-btn>
                    </ds-schedule-actions>
                </slot>
            </div>
            <!-- Title -->
            <slot name="scheduleTitle" v-bind="{schedule, schedule, calendarEvent, details}">
                <!-- class="ds-textfield ds-calendar-event-title" -->
                <v-text-field single-line hide-details solo flat
                              class="ds-event-title"
                              :label="labels.title"
                              :readonly="isReadOnly"
                              v-model="details.TieuDe"></v-text-field>
            </slot>
        </div>
        <div class="ds-event-body ds-event-area">
            <slot name="schedule" v-bind="slotData">
                <ds-schedule :schedule="schedule"
                             :day="day"
                             :read-only="readOnly"></ds-schedule>
            </slot>
        </div>
        <!-- Tabs -->
        <v-layout row v-if="hasTabs">
            <v-flex xs12 class="mt-2">
                <v-tabs class="text--primary" v-model="tab">
                    <v-tab href="#details" v-if="hasDetails">
                        {{ labels.tabs.details }}
                    </v-tab>
                    <v-tab href="#forecast" v-if="showForecast">
                        {{ labels.tabs.forecast }}
                    </v-tab>
                    <v-tab href="#exclusions" v-if="showExclusions">
                        {{ labels.tabs.removed }}
                    </v-tab>
                    <v-tab href="#inclusions" v-if="showInclusions">
                        {{ labels.tabs.added }}
                    </v-tab>
                    <v-tab href="#cancelled" v-if="showCancels">
                        {{ labels.tabs.cancelled }}
                    </v-tab>
                    <slot name="eventTabsExtra" v-bind="slotData"></slot>
                    <!-- Details -->
                    <v-tab-item value="details" v-if="hasDetails">
                        <v-card flat>
                            <v-card-text>
                                <!-- Location -->
                                <slot name="eventDetailsLocation" v-bind="slotData">
                                    <v-text-field v-if="$dayspan.supports.location"
                                                  single-line hide-details solo flat
                                                  prepend-icon="location_on"
                                                  :label="labels.location"
                                                  :readonly="isReadOnly"
                                                  v-model="details.DiaDiem"></v-text-field>
                                </slot>
                                <!-- Description -->
                                <slot name="eventDetailsDescription" v-bind="slotData">
                                    <v-textarea v-if="$dayspan.supports.description"
                                                hide-details single-line solo flat
                                                prepend-icon="subject"
                                                :label="labels.description"
                                                :readonly="isReadOnly"
                                                v-model="details.NoiDung"></v-textarea>
                                </slot>
                                <!-- Calendar -->
                                <slot name="eventDetailsCalendar" v-bind="slotData">
                                    <v-text-field v-if="$dayspan.supports.calendar"
                                                  single-line hide-details solo flat readonly
                                                  prepend-icon="event"
                                                  :label="labels.calendar"
                                                  :readonly="isReadOnly"
                                                  v-model="details.calendar"></v-text-field>
                                </slot>
                                <!-- Color -->
                                <slot name="eventDetailsColor" v-bind="slotData">
                                    <v-select v-if="$dayspan.supports.color"
                                              single-line hide-details solo flat
                                              prepend-icon="invert_colors"
                                              :items="$dayspan.colors"
                                              :color="details.color"
                                              :disabled="isReadOnly"
                                              v-model="details.color">
                                        <template slot="item" slot-scope="{ item }">
                                            <v-list-tile-content>
                                                <div class="ds-color-option" :style="{backgroundColor: item.value}" v-text="item.text"></div>
                                            </v-list-tile-content>
                                        </template>
                                    </v-select>
                                </slot>
                                <!-- Icon -->
                                <slot name="eventDetailsIcon" v-bind="slotData">
                                    <v-select v-if="$dayspan.supports.icon"
                                              single-line hide-details solo flat
                                              :prepend-icon="details.icon || 'help'"
                                              :items="$dayspan.icons"
                                              :disabled="isReadOnly"
                                              v-model="details.icon">
                                        <template slot="item" slot-scope="{ item }">
                                            <v-list-tile-avatar>
                                                <v-icon>{{ item.value }}</v-icon>
                                            </v-list-tile-avatar>
                                            <v-list-tile-content>
                                                {{ item.text }}
                                            </v-list-tile-content>
                                        </template>
                                    </v-select>
                                </slot>
                                <!-- Busy -->
                                <slot name="eventDetailsBusy" v-bind="slotData">
                                    <v-select v-if="$dayspan.supports.busy"
                                              single-line hide-details solo flat
                                              prepend-icon="work"
                                              :items="busyOptions"
                                              :disabled="isReadOnly"
                                              v-model="details.busy"></v-select>
                                </slot>
                                <slot name="eventDetailsExtra" v-bind="slotData"></slot>
                            </v-card-text>
                        </v-card>
                    </v-tab-item>
                    <!-- Forecast -->
                    <v-tab-item id="forecast" lazy v-if="showForecast">
                        <v-card flat>
                            <v-card-text>
                                <slot name="eventForecast" v-bind="slotData">
                                    <ds-schedule-forecast :schedule="schedule"
                                                          :day="day"
                                                          :read-only="readOnly"></ds-schedule-forecast>
                                </slot>
                            </v-card-text>
                        </v-card>
                    </v-tab-item>
                    <!-- Exclusions -->
                    <v-tab-item id="exclusions" lazy v-if="showExclusions">
                        <v-card flat>
                            <v-card-text>
                                <slot name="eventExclusions" v-bind="slotData">
                                    <ds-schedule-modifier :description="labels.exclusions"
                                                          :modifier="schedule.exclude"
                                                          :read-only="readOnly"></ds-schedule-modifier>
                                </slot>
                            </v-card-text>
                        </v-card>
                    </v-tab-item>
                    <!-- Inclusions -->
                    <v-tab-item id="inclusions" lazy v-if="showInclusions">
                        <v-card flat>
                            <v-card-text>
                                <slot name="eventInclusions" v-bind="slotData">
                                    <ds-schedule-modifier :description="labels.inclusions"
                                                          :modifier="schedule.include"
                                                          :read-only="readOnly"></ds-schedule-modifier>
                                </slot>
                            </v-card-text>
                        </v-card>
                    </v-tab-item>
                    <!-- Cancelled -->
                    <v-tab-item id="cancelled" lazy v-if="showCancels">
                        <v-card flat>
                            <v-card-text>
                                <slot name="eventCancels" v-bind="slotData">
                                    <ds-schedule-modifier :description="labels.cancelled"
                                                          :modifier="schedule.cancel"
                                                          :read-only="readOnly"></ds-schedule-modifier>
                                </slot>
                            </v-card-text>
                        </v-card>
                    </v-tab-item>
                    <slot name="eventTabItemsExtra" v-bind="slotData"></slot>
                </v-tabs>
            </v-flex>
        </v-layout>
    </div>
</template>

<script>
import { CalendarEvent, Calendar, Pattern } from 'dayspan';


export default {

  name: 'dsCalendarEventPopover',

  props:
  {
    calendarEvent:
    {
      required: true,
      type: CalendarEvent
    },

    calendar:
    {
      required: true,
      type: Calendar
    },

    readOnly:
    {
      type: Boolean,
      default: false
    },

    edit:
    {
      type: Function
    },

    allowEditOnReadOnly:
    {
      type: Boolean,
      default() {
        return this.$dsDefaults().allowEditOnReadOnly;
      }
    },

    close:
    {
      type: Function
    },

    formats:
    {
      validate(x) {
        return this.$dsValidate(x, 'formats');
      },
      default() {
        return this.$dsDefaults().formats;
      }
    },

    labels:
    {
      validate(x) {
        return this.$dsValidate(x, 'labels');
      },
      default() {
        return this.$dsDefaults().labels;
      }
    }
  },

  computed:
  {
    slotData()
    {
      return {
        calendarEvent: this.calendarEvent,
        calendar: this.calendar,
        edit: this.edit,
        close: this.close,
        details: this.details,
        readOnly: this.readOnly
      };
    },

    classes()
    {
      return {
        'ds-event-cancelled': this.calendarEvent.cancelled
      };
    },

    styleHeader()
    {
      return {
        backgroundColor: this.details.color,
        color: this.details.forecolor
      };
    },

    styleButton()
    {
      return {
        color: this.details.forecolor
      };
    },

    startDate()
    {
      return this.calendarEvent.start.format( this.formats.start );
    },

    busyness()
    {
      return this.details.busy ? this.labels.busy : this.labels.free;
    },

    hasBusy()
    {
      return typeof this.details.busy === 'boolean';
    },

    occurs()
    {
      return this.$dayspan.getEventOccurrence(
        this.calendarEvent.schedule,
        this.calendarEvent.start,
        this.labels,
        this.formats
      );
    },

    details()
    {
      return this.calendarEvent.event.data;
    },

    allowEdit()
    {
      return this.allowEditOnReadOnly || !this.isReadOnly;
    },

    isReadOnly()
    {
      return this.readOnly || this.$dayspan.readOnly || this.details.readonly;
    }
  },

  data: vm => ({

  }),

  methods:
  {

  }
}
</script>

<style scoped lang="scss">
.ds-calendar-event-popover-card {

  .v-btn--floating.v-btn--left {
    margin-left: 0px !important;

    .v-icon {
      height: auto;
    }
  }

  .v-card__text {
    padding: 16px 0;

    .v-list {

      .v-list__tile {
        padding: 0px !important;
        height: auto;

        .v-list__tile__sub-title {

        }
      }
    }
  }

  /deep/ .v-toolbar__extension {
    padding: 0 16px !important;

    .v-toolbar__title {
      margin-left: 56px;
    }
  }
}

</style>
