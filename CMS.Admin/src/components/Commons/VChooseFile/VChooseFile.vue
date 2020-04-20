<template>
    <div>
        <img v-if="filePath && type == 'image'" :src="filePath" alt="image preview" style="max-width:100%" />
        <v-text-field v-if="!multiple"
                      v-model="filePath"
                      :solo="solo"
                      :label="label"
                      type="text"
                      :hide-details="hideDetails"
                      append-outer-icon="add_photo_alternate"
                      @click:append-outer="chonFile"
                      :readonly="readonly"
                    :box="box"
                      :clearable="clearable">
        </v-text-field>
        <v-combobox v-model="filesPath" v-if="multiple"
                    :filter="filter"
                    :hide-no-data="!search"
                    :items="items"
                    :search-input.sync="search"
                    hide-selected
                    :label="label"
                    multiple
                    small-chips
                    :item-text="itemText"
                    :item-value="itemText"
                    @input="updateModel"
                    :box="box"
                      :solo="solo">
            <template slot="no-data">
                <v-list-tile>
                    <span class="subheading">Create</span>
                    <v-chip color="lighten-3"
                            class="text-truncate"
                            label
                            small>
                        {{ search }}
                    </v-chip>
                </v-list-tile>
            </template>
            <template v-if="item === Object(item)"
                      slot="selection"
                      slot-scope="{ item, parent, selected }">
                <v-chip color="lighten-3"
                        :selected="selected"
                        label
                        small>
                    <span class="pr-2 text-truncate">
                        {{ item[itemText] }}
                    </span>
                    <v-icon small
                            @click="parent.selectItem(item)">close</v-icon>
                </v-chip>
            </template>
            <template slot="item"
                      slot-scope="{ index, item, parent }">
                <v-list-tile-content>
                    <v-text-field v-if="editing === item"
                                  v-model="editing[itemText]"
                                  autofocus
                                  text
                                  background-color="transparent"
                                  hide-details
                                  solo
                                  @keyup.enter="edit(index, item)"></v-text-field>
                    <v-chip v-else
                            class="text-truncate"
                            color="lighten-3"
                            dark
                            label
                            small>
                        {{ item[itemText] }}
                    </v-chip>
                </v-list-tile-content>
                <v-spacer></v-spacer>
                <v-list-tile-action @click.stop>
                    <v-btn icon @click.stop.prevent="edit(index, item)">
                        <v-icon>{{ editing !== item ? 'edit' : 'check' }}</v-icon>
                    </v-btn>
                </v-list-tile-action>
            </template>
            <v-slide-x-reverse-transition slot="append-outer"
                                          mode="out-in">
                <v-icon color="info" @click="chonFile">add_photo_alternate</v-icon>
            </v-slide-x-reverse-transition>
        </v-combobox>
    </div>
</template>
<script>
    export default {
        name: 'VChooseFile',
        props: {
			label: {
                type: String,
                default: ''
            },
			clearable: {
                type: Boolean,
                default: false
            },
			hideDetails: {
                type: String,
                default: ''
            },
            multiple: {
                type: Boolean,
                default: false
            },
			solo: {
                type: Boolean,
                default: false
            },
            box: {
                type: Boolean,
                default: false
            },
			readonly: {
                type: Boolean,
                default: false
            },
            name: {
                type: String,
                default: ''
            },
            value: '',
            id: {
                type: String,
                default: ''
            },
            type: {
                type: String,
                default: 'file'
            },
            itemText: {
                type: String,
                default: 'text'
            },
            itemValue: {
                type: String,
                default: 'value'
            }
        },
        data() {
            return {
                filePath: '',
                activator: null,
                attach: null,
                editing: null,
                index: -1,
                items: [],
                nonce: 1,
                menu: false,
                filesPath: [],
                x: 0,
                search: null,
                y: 0,
            };
        },
        computed: {
        },
        watch: {
            filePath: function (val) {
                if (this.multiple) {
                    if (this.filesPath == null)
                        this.filesPath = []
                    this.filesPath.push({
                        [this.itemText]: val
                    })
                }
                else {
                    this.$emit('input', val)
                }
			},
			value: function (val) {
                if (this.multiple) {
                    this.filesPath = val
                }
                else {
                    this.filePath = val
                }
            },
            filesPath(val, prev) {
                if (val == null || val == undefined)
                    val = []
                if (prev == null || prev == undefined)
                    prev = []
                if (val.length === prev.length) return

                this.filesPath = val.map(v => {
                    if (typeof v === 'string') {
                        v = {
                            [this.itemText]: v
                        }

                        this.items.push(v)

                        this.nonce++
                    }
                    return v
                })
                this.$emit('input', this.filesPath)
                console.log(this.filesPath)
            }
        },
        mounted() {
        },
        beforeDestroy() {
        },
        methods: {
            chonFile() {
                let config = {
                }
                if (this.$store.state.user
                    && this.$store.state.user.AccessToken
                    && this.$store.state.user.AccessToken.Token) {
                    config.connectorInfo = 'token=' + this.$store.state.user.AccessToken.Token;
                }
                var finder = new CKFinder(config)
				finder.basePath = '../../images'
                finder.selectActionFunction = this.selectActionFunction
                finder.popup()
			},
			selectActionFunction (fileUrl) {
				this.filePath = fileUrl
            },
            edit(index, item) {
                if (!this.editing) {
                    this.editing = item
                    this.index = index
                } else {
                    this.editing = null
                    this.index = -1
                }
            },
            filter(item, queryText, itemText) {
                if (item.header) return false

                const hasValue = val => val != null ? val : ''

                const text = hasValue(itemText)
                const query = hasValue(queryText)

                return text.toString()
                    .toLowerCase()
                    .indexOf(query.toString().toLowerCase()) > -1
            },
            updateModel(val) {
            }
        }
    };
</script>
