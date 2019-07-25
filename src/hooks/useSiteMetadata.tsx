import { graphql, useStaticQuery } from 'gatsby';
import { SiteMetadataQuery, SiteSiteMetadata } from '../graphqlTypes';

const useSiteMetaData = () => {
    const { site }: SiteMetadataQuery = useStaticQuery(graphql`
        query SiteMetadata {
            site {
                siteMetadata {
                    description
                    title
                }
            }
        }
    `);

    return site && site.siteMetadata;
};

export default useSiteMetaData;
