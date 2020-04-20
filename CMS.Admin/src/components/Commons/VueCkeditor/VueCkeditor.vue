<template>
    <div class="ckeditor">
        <textarea :name="name"
                  :id="id"
                  :value="value"
                  :types="types"
                  :config="config"
                  :disabled="readOnlyMode">
    </textarea>
    </div>
</template>
<script>
    let inc = new Date().getTime();

    export default {
        name: 'VueCkeditor',
        props: {
            name: {
                type: String,
                default: () => `editor-${++inc}`
            },
            value: {
                type: String
            },
            id: {
                type: String,
                default: () => `editor-${inc}`
            },
            types: {
                type: String,
                default: () => `classic`
            },
            config: {
                type: Object,
                default: () => {
                }
            },
            instanceReadyCallback: {
                type: Function
            },
            readOnlyMode: {
                type: Boolean,
                default: () => false
            }
        },
        data() {
            return {
                instanceValue: ''
            };
        },
        computed: {
            instance() {
                return CKEDITOR.instances[this.id];
            },
            token() {
                if (this.$store.state.user
                    && this.$store.state.user.AccessToken
                    && this.$store.state.user.AccessToken.Token) {
                    return this.$store.state.user.AccessToken.Token;
                }
                return '';
            }
        },
        watch: {
            value(val) {
                try {
                    if (this.instance) {
                        this.update(val);
                    }
                } catch (e) {
                    console.log(e)
                }
            },
            readOnlyMode(val) {
                this.instance.setReadOnly(val);
            }
        },
        mounted() {
            this.create();
        },
        beforeDestroy() {
            this.destroy();
        },
        methods: {
            create() {
                if (typeof CKEDITOR === 'undefined') {
                    console.log('CKEDITOR is missing (http://ckeditor.com/)');
                } else {
                    if (this.config) {
                        this.config.filebrowserBrowseUrl = '/lib/ckfinder/ckfinder.html?connectorInfo=token=' + this.token;
                        this.config.filebrowserImageBrowseUrl = '/lib/ckfinder/ckfinder.html?type=Images&connectorInfo=token=' + this.token;
                        this.config.filebrowserFlashBrowseUrl = '/lib/ckfinder/ckfinder.html?type=Flash&connectorInfo=token=' + this.token;
                        this.config.filebrowserUploadUrl = '/lib/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files&connectorInfo=token=' + this.token;
                        this.config.filebrowserImageUploadUrl = '/lib/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images&connectorInfo=token=' + this.token;
                        this.config.filebrowserFlashUploadUrl = '/lib/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash&connectorInfo=token=' + this.token;
                    }
                    if (this.types === 'inline') {
                        CKEDITOR.inline(this.id, this.config);
                    } else {
                        CKEDITOR.replace(this.id, this.config);
                    }

                    this.instance.setData(this.value);

                    this.instance.on('instanceReady', () => {
                        this.instance.setData(this.value);
                    });

                    // Ckeditor change event
                    this.instance.on('change', this.onChange);

                    // Ckeditor mode html or source
                    this.instance.on('mode', this.onMode);

                    // Ckeditor blur event
                    this.instance.on('blur', evt => {
                        this.$emit('blur', evt);
                    });

                    // Ckeditor focus event
                    this.instance.on('focus', evt => {
                        this.$emit('focus', evt);
                    });

                    // Ckeditor contentDom event
                    this.instance.on('contentDom', evt => {
                        this.$emit('contentDom', evt);
                    });

                    // Ckeditor dialog definition event
                    CKEDITOR.on('dialogDefinition', evt => {
                        this.$emit('dialogDefinition', evt);

                        // Take the dialog name and its definition from the event data
                        var dialogName = evt.data.name;
                        var dialogDefinition = evt.data.definition;
                        if (dialogName == 'image' || dialogName == 'link') {
                            // Remove upload tab
                            dialogDefinition.removeContents('Upload');
                            dialogDefinition.removeContents('upload');
                        }
                    });

                    // Ckeditor file upload request event
                    this.instance.on('fileUploadRequest', evt => {
                        this.$emit('fileUploadRequest', evt);
                    });

                    // Ckditor file upload response event
                    this.instance.on('fileUploadResponse', evt => {
                        setTimeout(() => {
                            this.onChange();
                        }, 0);
                        this.$emit('fileUploadResponse', evt);
                    });
                    // Listen for instanceReady event
                    if (typeof this.instanceReadyCallback !== 'undefined') {
                        this.instance.on('instanceReady', this.instanceReadyCallback);
                    }
                    if (typeof CKFinder !== undefined) {
                        let config = {
                            basePath: '/lib/ckfinder',
                            config: {
                                connectorInfo: '',
                                removeDialogTabs: 'link:upload;image:Upload'
                            }
                        };

                        //if (this.$store.state.user
                        //    && this.$store.state.user.AccessToken
                        //    && this.$store.state.user.AccessToken.Token) {
                        //    config.config.connectorInfo = 'token=' + this.$store.state.user.AccessToken.Token;
                        //}
                        // CKFinder.setupCKEditor(this.instance, '/lib/ckfinder');
                        CKFinder.setupCKEditor(this.instance, config);
                    }
                }
            },
            update(val) {
                if (this.instanceValue !== val) {
                    this.instance.setData(val, { internal: false });
                }
            },
            destroy() {
                try {
                    let editor = window['CKEDITOR'];
                    if (editor.instances) {
                        for (let instance in editor.instances) {
                            instance.destroy();
                        }
                    }
                } catch (e) {
                    console.log(e)
                }
            },
            onMode() {
                if (this.instance.mode === 'source') {
                    let editable = this.instance.editable();
                    editable.attachListener(editable, 'input', () => {
                        this.onChange();
                    });
                }
            },
            onChange() {
                let html = this.instance.getData();
                if (html !== this.value) {
                    this.$emit('input', html);
                    this.instanceValue = html;
                }
            }
        }
    };
</script>
