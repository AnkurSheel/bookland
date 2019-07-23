import { graphql, useStaticQuery } from 'gatsby';

interface SiteMetaDataProps {
    site: {
        siteMetadata: {
            title: string;
            description: string;
        };
    };
}

const useSiteMetaData = () => {
    const data: SiteMetaDataProps = useStaticQuery(graphql`
        query MyQuery {
            site {
                siteMetadata {
                    description
                    title
                }
            }
        }
    `);
    return data.site.siteMetadata;
};

export default useSiteMetaData;
